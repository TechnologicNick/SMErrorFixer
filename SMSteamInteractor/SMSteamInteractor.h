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

	bool m_LoopRunning = false;
	void LoopRunCallbacks();

	void DownloadSubscribedItems();
	void SendQueryUGCRequests();
	int UnsubscribeAccessDenied();

	std::vector<PublishedFileId_t> m_SubscribedItems;
	int m_NumSubscribedItems;
	int m_CurrentPage = 0;

	std::vector<SteamUGCDetails_t> m_SubscribedItemDetails;
	bool m_RequireUnsubscribeConfirmation = true;

private:
	CSteamInteractor *m_pSteamInteractor;

	STEAM_CALLBACK(CSteamInteractor, OnDownloadItemResult, DownloadItemResult_t);
	//void OnDownloadItemResult(DownloadItemResult_t pCallback);
	
	bool SendQueryUGCRequestPage(int page);
	void OnSteamUGCQueryCompleted(SteamUGCQueryCompleted_t *pCallback, bool bIOFailure);
	CCallResult<CSteamInteractor, SteamUGCQueryCompleted_t> m_SteamUGCQueryCompletedCallResult;
	void OnAllSteamUGCQueriesCompleted();
};
