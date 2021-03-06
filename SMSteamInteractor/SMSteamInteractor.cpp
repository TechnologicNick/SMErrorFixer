// SMSteamInteractor.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "SMSteamInteractor.h"

int main(int argc, char* argv[])
{
	// Art generated by http://patorjk.com/software/taag/#p=display&f=Standard&t=SMSteamInteractor
	std::cout << R"( ____  __  __ ____  _                       ___       _                      _             )" << std::endl;
	std::cout << R"(/ ___||  \/  / ___|| |_ ___  __ _ _ __ ___ |_ _|_ __ | |_ ___ _ __ __ _  ___| |_ ___  _ __ )" << std::endl;
	std::cout << R"(\___ \| |\/| \___ \| __/ _ \/ _` | '_ ` _ \ | || '_ \| __/ _ \ '__/ _` |/ __| __/ _ \| '__|)" << std::endl;
	std::cout << R"( ___) | |  | |___) | ||  __/ (_| | | | | | || || | | | ||  __/ | | (_| | (__| || (_) | |   )" << std::endl;
	std::cout << R"(|____/|_|  |_|____/ \__\___|\__,_|_| |_| |_|___|_| |_|\__\___|_|  \__,_|\___|\__\___/|_|   )" << "by TechnologicNick" << std::endl << std::endl;
    
	std::cout << "Run with \"-unsubscribe\" to skip the unsubscribe confirmation." << std::endl << std::endl;

	CSteamInteractor si;

	si.Init();

	for (int i = 0; i < argc; i++) {
		if (strcmp(argv[i], "-unsubscribe") == 0) {
			si.m_RequireUnsubscribeConfirmation = false;
		}
	}

	si.DownloadSubscribedItems();

	si.SendQueryUGCRequests();

	si.LoopRunCallbacks();

	si.Shutdown();
}


CSteamInteractor::CSteamInteractor() {
	
}

void CSteamInteractor::Init() {
	if (!SteamAPI_Init())
	{
		printf("SteamAPI_Init() failed\n");
	}
}

void CSteamInteractor::Shutdown() {
	printf("Shutting down the SteamAPI");
	SteamAPI_Shutdown();
}




void CSteamInteractor::DownloadSubscribedItems() {
	int count = SteamUGC()->GetNumSubscribedItems();
	m_SubscribedItems = std::vector<PublishedFileId_t>(count);
	m_NumSubscribedItems = SteamUGC()->GetSubscribedItems(m_SubscribedItems.data(), count);

	printf("Number of subscribed items: %d\n", count);
}

void CSteamInteractor::SendQueryUGCRequests() {
	SendQueryUGCRequestPage(0);
}

bool CSteamInteractor::SendQueryUGCRequestPage(int page) {
	if (page < 0 || page > m_NumSubscribedItems / kNumUGCResultsPerPage) {
		printf("Page %d is out of bounds! (between 0 and %d)\n", page, m_NumSubscribedItems / kNumUGCResultsPerPage);
		return false;
	}


	printf("Sending QueryUGCRequest for page %d\n", page);

	int i = page * kNumUGCResultsPerPage;

	std::vector<PublishedFileId_t> itemBatch(m_SubscribedItems.begin() + i, m_SubscribedItems.begin() + std::min(m_SubscribedItems.size(), i + kNumUGCResultsPerPage));

	UGCQueryHandle_t hDetails = SteamUGC()->CreateQueryUGCDetailsRequest(itemBatch.data(), itemBatch.size());
	if (hDetails == k_UGCQueryHandleInvalid) {
		printf("CreateQueryUGCDetailsRequest failed\n");
		return false;
	}

	SteamAPICall_t hSteamAPICall = SteamUGC()->SendQueryUGCRequest(hDetails);
	if (hSteamAPICall == k_UGCQueryHandleInvalid) {
		printf("SendQueryUGCRequest failed\n");
		return false;
	}

	m_SteamUGCQueryCompletedCallResult.Set(hSteamAPICall, this, &CSteamInteractor::OnSteamUGCQueryCompleted);

	//printf("hDetails = %p, hSteamAPICall = %p\n", hDetails, hSteamAPICall);
	return true;
}

int CSteamInteractor::UnsubscribeAccessDenied() {
	int sum = 0;

	for (SteamUGCDetails_t details : m_SubscribedItemDetails) {

		if (details.m_eResult == k_EResultAccessDenied) {
			printf("Unsubscribing from %llu\n", details.m_nPublishedFileId);

			SteamUGC()->UnsubscribeItem(details.m_nPublishedFileId);
			sum++;
		}

	}

	printf("Unsubscribed from %d item%s\n", sum, sum != 1 ? "s" : "");
	return sum;
}

void CSteamInteractor::LoopRunCallbacks() {
	m_LoopRunning = true;

	while (m_LoopRunning) {
		SteamAPI_RunCallbacks();

		std::this_thread::sleep_for(std::chrono::milliseconds(100));
	}
}

void CSteamInteractor::OnDownloadItemResult(DownloadItemResult_t* pCallback) {
	printf("Result for %llu (AppID = %d): %d \n", pCallback->m_nPublishedFileId, pCallback->m_unAppID, pCallback->m_eResult);
}

void CSteamInteractor::OnSteamUGCQueryCompleted(SteamUGCQueryCompleted_t* pCallback, bool bIOFailure) {
	//printf("[OnSteamUGCQueryCompleted] bIOFailure = %s, m_handle = %p, m_eResult = %d, m_unNumResultsReturned = %d, m_unTotalMatchingResults = %d, m_bCachedData = %d\n", bIOFailure ? "true" : "false", pCallback->m_handle, pCallback->m_eResult, pCallback->m_unNumResultsReturned, pCallback->m_unTotalMatchingResults, pCallback->m_bCachedData);
	
	if (!bIOFailure) {
		printf("[SteamUGCQueryCompleted] Results for page %d\n", m_CurrentPage);

		for (int i = 0; i < pCallback->m_unNumResultsReturned; i++) {
			SteamUGCDetails_t details;
			SteamUGC()->GetQueryUGCResult(pCallback->m_handle, i, &details);

			printf("[%llu]\t result = %d\ttitle = %s\t\n", details.m_nPublishedFileId, details.m_eResult, details.m_rgchTitle);
			m_SubscribedItemDetails.push_back(details);
		}

		if (++m_CurrentPage <= m_NumSubscribedItems / kNumUGCResultsPerPage) {
			SendQueryUGCRequestPage(m_CurrentPage);
		} else {
			OnAllSteamUGCQueriesCompleted();
		}
		
	} else {
		printf("[SteamUGCQueryCompleted] IOFailure for page %d\n", m_CurrentPage);
	}
	
}

void CSteamInteractor::OnAllSteamUGCQueriesCompleted() {
	printf("All Steam UGC queries have been completed\n");

	if (m_RequireUnsubscribeConfirmation) {
		printf("Do you want to unsubscribe from workshop items you don't have access to? (Y/N)\n");

		char input;
		std::cin >> input;

		if (input == 'Y' || input == 'y') {
			UnsubscribeAccessDenied();
		}
	} else {
		printf("Already confirmed to unsubscribe\n");
		UnsubscribeAccessDenied();
	}

	m_LoopRunning = false;
}


