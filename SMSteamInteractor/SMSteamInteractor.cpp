// SMSteamInteractor.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "SMSteamInteractor.h"

int main()
{
    std::cout << "SMSteamInteractor by TechnologicNick\n\n";

	CSteamInteractor si;

	si.Init();

	si.DownloadSubscribedItems();

	si.SendQueryUGCRequestPage(0);

	si.LoopRunCallbacks();

	si.Shutdown();
}


//CSteamInteractor::CSteamInteractor(CSteamInteractor *pSteamInteractor) : m_pSteamInteractor(pSteamInteractor){
CSteamInteractor::CSteamInteractor() {
	printf("Created new CSteamInteractor\n");
}

void CSteamInteractor::Init() {
	if (!SteamAPI_Init())
	{
		printf("SteamAPI_Init() failed\n");
	}
}

void CSteamInteractor::Shutdown() {
	SteamAPI_Shutdown();
}




void CSteamInteractor::DownloadSubscribedItems() {
	int count = SteamUGC()->GetNumSubscribedItems();
	m_SubscribedItems = std::vector<PublishedFileId_t>(count);
	m_NumSubscribedItems = SteamUGC()->GetSubscribedItems(m_SubscribedItems.data(), count);

	printf("Number of subscribed items: %d\n", count);

	//for (int i = 0; i < count; i++) {
	//	PublishedFileId_t item = items[i];
	//
	//	//if (item != (uint64)2055627758) continue; //Test item
	//
	//	printf("[%d/%d] Doing something with %llu...\n", i + 1, count, item);
	//	//printf("%s\n", SteamUGC()->DownloadItem(item, true) ? "true" : "false");
	//
	//
	//
	//	//break;
	//}
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

void CSteamInteractor::LoopRunCallbacks() {
	while (true) {
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

			printf("[%llu]\t result = %d\t visibility = %d\ttitle = %s\t\n", details.m_nPublishedFileId, details.m_eResult, details.m_eVisibility, details.m_rgchTitle);
		}

		SendQueryUGCRequestPage(++m_CurrentPage);
	} else {
		printf("[SteamUGCQueryCompleted] IOFailure for page %d\n", m_CurrentPage);
	}
	
}


