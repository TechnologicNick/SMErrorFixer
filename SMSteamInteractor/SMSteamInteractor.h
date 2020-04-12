#pragma once

#include <stdio.h>
#include <algorithm>
#include <iostream>
#include <vector>
#include <chrono>
#include <thread>
#include "Steam/steam_api.h"

class CSteamInteractor {
public:
	//CSteamInteractor(CSteamInteractor *pSteamInteractor);
	CSteamInteractor();
	//CSteamInteractor(const CSteamInteractor &) = default;

	void Init();
	void Shutdown();

	void LoopRunCallbacks();

	void DownloadSubscribedItems();
	bool SendQueryUGCRequestPage(int page);

	std::vector<PublishedFileId_t> m_SubscribedItems;
	int m_NumSubscribedItems;
	int m_CurrentPage = 0;

private:
	CSteamInteractor *m_pSteamInteractor;

	STEAM_CALLBACK(CSteamInteractor, OnDownloadItemResult, DownloadItemResult_t);
	//void OnDownloadItemResult(DownloadItemResult_t pCallback);
	
	void OnSteamUGCQueryCompleted(SteamUGCQueryCompleted_t *pCallback, bool bIOFailure);
	CCallResult<CSteamInteractor, SteamUGCQueryCompleted_t> m_SteamUGCQueryCompletedCallResult;
};
