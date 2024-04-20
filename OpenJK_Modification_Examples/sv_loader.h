#pragma once


typedef struct loaderImports_s {
	void		(*InitGame)							(int levelTime, int randomSeed, int restart, void* dllHandle);
	void		(*ShutdownGame)						(int restart);
	char* (*ClientConnect)					(int clientNum, qboolean firstTime, qboolean isBot);
	void		(*ClientBegin)						(int clientNum, qboolean allowTeamReset);
	qboolean(*ClientUserinfoChanged)			(int clientNum);
	void		(*ClientDisconnect)					(int clientNum);
	void		(*ClientCommand)					(int clientNum);
	void		(*ClientThink)						(int clientNum, usercmd_t* ucmd);
	void		(*RunFrame)							(int levelTime);
	qboolean(*ConsoleCommand)						(void);
	void		(*GameDataLocationAcquired)			(sharedEntity_t* gEnts, int numGEntities, int sizeofGEntity_t, playerState_t* clients, int sizeofGClient);
} loaderImports_t;