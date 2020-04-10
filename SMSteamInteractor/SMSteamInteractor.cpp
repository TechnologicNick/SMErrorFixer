// SMSteamInteractor.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "SMSteamInteractor.h"

int main()
{
    std::cout << "Hello World!\n";


	CSteamInteractor::Init();

	CSteamInteractor::DownloadSubscribedItems();



	CSteamInteractor::Shutdown();
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
	std::vector<PublishedFileId_t> items(count);
	count = SteamUGC()->GetSubscribedItems(items.data(), count);

	for (int i = 0; i < count; i++) {
		PublishedFileId_t item = items[i];

		printf("[%d/%d] Downloading %lu\t", i + 1, count, item);
		printf("%s\n", SteamUGC()->DownloadItem(item, true) ? "true" : "false");

		break;
	}

	printf("Number of subscribed items: %d\n", count);

	while (true) {
		SteamAPI_RunCallbacks();

		//std::cout << "running" << std::endl;

		std::this_thread::sleep_for(std::chrono::milliseconds(100));
	}
}

void CSteamInteractor::OnDownloadItemResult(DownloadItemResult_t* pCallback) {
	printf("Result for %lu: %d\n", pCallback->m_nPublishedFileId, pCallback->m_eResult);
}



