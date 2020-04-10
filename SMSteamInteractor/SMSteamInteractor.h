#pragma once

#include <iostream>
#include <vector>
#include <chrono>
#include <thread>
#include "Steam/steam_api.h"

class CSteamInteractor {
public:
	static void Init();
	static void Shutdown();

	static void DownloadSubscribedItems();

private:
	STEAM_CALLBACK(CSteamInteractor, OnDownloadItemResult, DownloadItemResult_t);
	static void OnDownloadItemResult(DownloadItemResult_t pCallback);
	
};