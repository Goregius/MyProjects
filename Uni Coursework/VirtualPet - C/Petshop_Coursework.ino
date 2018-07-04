#include <TimeLib.h>
#include <Time.h>
#include <Adafruit_RGBLCDShield.h>
#include <avr/eeprom.h>

Adafruit_RGBLCDShield lcd = Adafruit_RGBLCDShield();

#pragma region defines

#define RED 0x1
#define YELLOW 0x3
#define GREEN 0x2
#define TEAL 0x6
#define BLUE 0x4
#define VIOLET 0x5
#define WHITE 0x7

#define MODE_STARTUP 0
#define MODE_GAMEPLAY 1
#define MODE_MENU 2

#define SAVE_NULL 0
#define SAVE_PRESENT 1

#define STARTUP_STATE_MAIN 0
#define STARTUP_STATE_LOAD 1
#define STARTUP_STATE_NEW 2

#define PET_MAX_AGE 600

#define HAPPINESS_0 ":("
#define HAPPINESS_1 ":)"
#define HAPPINESS_2 ":D"

#define STAGE_0 ""
#define STAGE_1 "="
#define STAGE_2 "=="

#define FULLNESS_0 "--"
#define FULLNESS_1 "-o"
#define FULLNESS_2 "oo"
#define FULLNESS_3 "oO"
#define FULLNESS_4 "OO"

#define ACTION_FEED 1
#define ACTION_PLAY 2
#define ACTION_GROW 3

#define ACTION_VALID ">"
#define ACTION_INVALID "X"

#define MENU_STATE_MAIN 0
#define MENU_STATE_SAVE 1
#define MENU_STATE_DELETE 2
#define MENU_STATE_NEW 3

#define MENU_OPTION_SAVE 0
#define MENU_OPTION_NEW 1
#define MENU_OPTION_DELETE 2
#define MENU_OPTION_LEAVE 3
#pragma endregion

struct age
{
	int(*seconds)();
	int(*minutes)();
};

struct pet
{
	char stage;
	char happiness;
	char fullness;
	age petAge;
};

pet mainPet;
time_t stoppedTime = now();
int nextSecond = 0;

bool buttonPressed = false;

char startupState = STARTUP_STATE_MAIN;
char mode = MODE_STARTUP;
char selectedAction = ACTION_FEED;
char menuState = MENU_STATE_MAIN;
char selectedOption = MENU_OPTION_SAVE;

bool isStartupConfirmYes = true;
bool isMenuConfirmYes = true;


void setup()
{
	assignGlobals();

	lcd.begin(16, 2);
	lcd.noBlink();

	byte savePresent = eeprom_read_byte((uint8_t*)0);

	switch (savePresent)
	{
	case SAVE_PRESENT:
		setupStartup();
		break;
	case SAVE_NULL:
		setupMainGameplay(false);
		break;
	default:
		eeprom_write_byte((uint8_t*)0, SAVE_NULL);
		setupMainGameplay(false);
		break;
	}
}

void assignGlobals() {
	startupState = STARTUP_STATE_MAIN;

	mode = MODE_STARTUP;
	nextSecond = 0;
	buttonPressed = false;
	selectedAction = ACTION_FEED;

	menuState = MENU_STATE_MAIN;
	selectedOption = MENU_OPTION_SAVE;
	stoppedTime = now();

	isStartupConfirmYes = true;
	isMenuConfirmYes = true;
}

pet loadPet() {
	pet p;
	p.stage = eeprom_read_byte((uint8_t*)1);
	p.happiness = eeprom_read_byte((uint8_t*)2);
	p.fullness = eeprom_read_byte((uint8_t*)3);
	p.petAge.seconds = second;
	p.petAge.minutes = minute;
	
	uint32_t seconds = eeprom_read_dword((uint32_t*)4);
	uint32_t minutes = eeprom_read_dword((uint32_t*)8);
	setTime(0, minutes, seconds, 0, 0, 0);

	return p;
}

void loop()
{
	switch (mode)
	{
	case MODE_STARTUP:
		runStartup();
		break;
	case MODE_GAMEPLAY:
		runMainGameplay();
		break;
	case MODE_MENU:
		runMenu();
		break;
	}	
}

void setupStartup() {
	mode = MODE_STARTUP;
	openStartupMainDialog();
}

void runStartup() {
	uint8_t buttons = lcd.readButtons();

	if (buttons)
	{
		if (!buttonPressed) {
			onStartupButtonPress(buttons);
		}
		buttonPressed = true;
	}
	else {
		buttonPressed = false;
	}
}

void onStartupButtonPress(uint8_t buttons) {
	if (buttons & BUTTON_RIGHT)
	{
		onStartupRightPressed();
	}
	else if (buttons & BUTTON_UP)
	{
		onStartupUpPressed();
	}
	else if (buttons & BUTTON_DOWN)
	{
		onStartupDownPressed();
	}
	else if (buttons & BUTTON_LEFT)
	{
		onStartupLeftPressed();
	}
	else if (buttons & BUTTON_SELECT)
	{
		onStartupSelectPressed();
	}
}

void onStartupRightPressed() {
	toggleConfirm();
}

void onStartupUpPressed() {
	if (startupState == STARTUP_STATE_MAIN) {
		openLoadDialog();
	}
}

void onStartupDownPressed() {
	if (startupState == STARTUP_STATE_MAIN) {
		openNewPetDialog();
	}
	else {
		openStartupMainDialog();
	}
}

void onStartupLeftPressed() {
	toggleConfirm();
}

void onStartupSelectPressed() {
	
	if (startupState == STARTUP_STATE_LOAD) {
		if (isStartupConfirmYes) {
			setupMainGameplay(true);
		}
		else {
			openStartupMainDialog();
		}
		
	}
	else if (startupState == STARTUP_STATE_NEW) {
		if (isStartupConfirmYes) {
			setupMainGameplay(false);
		}
		else {
			openStartupMainDialog();
		}
	}
}

void openStartupMainDialog() {
	startupState = STARTUP_STATE_MAIN;
	lcd.clear();
	lcd.setCursor(0, 0);
	lcd.print("LOAD PET? (UP)");
	lcd.setCursor(0, 1);
	lcd.print("NEW PET?  (DOWN)");
}

void openLoadDialog() {
	
	startupState = STARTUP_STATE_LOAD;
	isStartupConfirmYes = true;
	lcd.clear();
	pet p = loadPet();
	printStatus(&p);
	lcd.setCursor(0, 1);
	lcd.print("> LOAD");
}

void openNewPetDialog() {
	startupState = STARTUP_STATE_NEW;
	isStartupConfirmYes = true;
	lcd.clear();
	pet p = { 0, 2, 3,{ second, minute } };
	setTime(0, 0, 0, 0, 0, 0);
	printStatus(&p);
	lcd.setCursor(0, 1);
	lcd.print("> CREATE");
}

void toggleConfirm() {
	if (startupState == STARTUP_STATE_LOAD) {
		if (isStartupConfirmYes) {
			lcd.setCursor(0, 1);
			lcd.print("> GO BACK");
		}
		else {
			lcd.setCursor(0, 1);
			lcd.print("> LOAD   ");
		}
		isStartupConfirmYes = !isStartupConfirmYes;
	}
	else if (startupState == STARTUP_STATE_NEW) {
		if (isStartupConfirmYes) {
			lcd.setCursor(0, 1);
			lcd.print("> GO BACK");
		}
		else {
			lcd.setCursor(0, 1);
			lcd.print("> CREATE ");
		}
		isStartupConfirmYes = !isStartupConfirmYes;
	}
}

void setupMainGameplay(bool useLoad) {
	lcd.setBacklight(WHITE);
	mode = MODE_GAMEPLAY;
	if (useLoad) {
		mainPet = loadPet();
	}
	else {
		mainPet = {0, 2, 3, {second, minute}};
		setTime(0, 0, 0, 0, 0, 0);
	}

	pet* p = &mainPet;

	lcd.clear();
	onNewSecond(p);
	printAction(selectedAction);
	lcd.setCursor(0, 1);
}

void runMainGameplay() {
	pet* p = &mainPet;

	if (getPetAgeInSeconds(p->petAge) >= nextSecond)
	{
		onNewSecond(p);
	}

	uint8_t buttons = lcd.readButtons();

	if (buttons)
	{
		if (!buttonPressed) {
			onButtonPress(buttons);
		}
		buttonPressed = true;
	}
	else {
		buttonPressed = false;
	}
}

bool isPetMaxAge(age age) {
	return getPetAgeInSeconds(age) == PET_MAX_AGE;
}

void onNewSecond(pet* p)
{
	if (isPetMaxAge(p->petAge)) {
		pauseAgeing();
		
		printStatus(p);
		printAction(selectedAction);
		return;
	}

	nextSecond = getPetAgeInSeconds(p->petAge) + 1;

	int totalSeconds = getPetAgeInSeconds(p->petAge);
	if (totalSeconds == 5)
	{
		p->stage = 1;
	}

	if (p->stage >= 1)
	{
		const int secondsSince5 = totalSeconds - 5;
		if (secondsSince5 > 0)
		{
			if (secondsSince5 % 7 == 0)
			{
				reducePetHappiness(p);
			}
			if (secondsSince5 % 11 == 0)
			{
				reducePetFullness(p);
			}
		}
	}

	printStatus(p);
	printAction(selectedAction);
}

int getPetAgeInSeconds(age age)
{
	return age.seconds() + age.minutes() * 60;
}

void printStatus(pet* p)
{
	if (isPetMaxAge(p->petAge)) {
		lcd.setCursor(0, 0);
		lcd.print("X|");
	}
	else
		printHappiness(p->happiness, 0);

	printFullness(p->fullness, 2);
	printStage(p->stage, 4);

	lcd.setCursor(6, 0);
	printAge(p->petAge, 11);

}

void printHappiness(char happiness, char pos)
{
	lcd.setCursor(pos, 0);
	switch (happiness)
	{
	case 0:
		lcd.print(HAPPINESS_0);
		break;
	case 1:
		lcd.print(HAPPINESS_1);
		break;
	case 2:
		lcd.print(HAPPINESS_2);
		break;
	}
}

void printStage(char stage, char pos) {
	lcd.setCursor(pos, 0);
	switch (stage)
	{
	case 0:
		lcd.print(STAGE_0);
		break;
	case 1:
		lcd.print(STAGE_1);
		break;
	case 2:
		lcd.print(STAGE_2);
		break;
	}
}

void printFullness(char fullness, char pos) {
	lcd.setCursor(pos, 0);
	switch (fullness)
	{
	case 0:
		lcd.print(FULLNESS_0);
		break;
	case 1:
		lcd.print(FULLNESS_1);
		break;
	case 2:
		lcd.print(FULLNESS_2);
		break;
	case 3:
		lcd.print(FULLNESS_3);
		break;
	case 4:
		lcd.print(FULLNESS_4);
		break;
	}
}

void printAge(age age, char pos) {
	lcd.setCursor(pos, 0);
	if (age.minutes() < 10) {
		lcd.print("0");
		lcd.print(age.minutes());
	}
	else {
		lcd.print(age.minutes());
	}
	lcd.print(":");
	if (age.seconds() < 10) {
		lcd.print("0");
		lcd.print(age.seconds());
	}
	else {
		lcd.print(age.seconds());
	}
}

void printAction(char action) {
		lcd.setCursor(0, 1);
		switch (action)
		{
		case ACTION_FEED:
			if (isFeedValid(mainPet))
				lcd.print(ACTION_VALID);
			else
				lcd.print(ACTION_INVALID);
			lcd.setCursor(2, 1);
			lcd.print("FEED");
			break;
		case ACTION_GROW:
			if (isGrowValid(mainPet))
				lcd.print(ACTION_VALID);
			else
				lcd.print(ACTION_INVALID);
			lcd.setCursor(2, 1);
			lcd.print("GROW");
			break;
		case ACTION_PLAY:
			if (isPlayValid(mainPet))
				lcd.print(ACTION_VALID);
			else
				lcd.print(ACTION_INVALID);
			lcd.setCursor(2, 1);
			lcd.print("PLAY");
			break;
		}	
}

void clearCells(char column, char row, char length) {
	lcd.setCursor(column, row);
	for (char i = 0; i < length; i++)
		lcd.print(" ");
	lcd.setCursor(0, row);
}

void reducePetHappiness(pet* pet)
{
	if (pet->happiness > 0)
		--pet->happiness;
}

void reducePetFullness(pet* pet)
{
	if (pet->fullness > 0) {
		--pet->fullness;
		if (pet->fullness == 0)
			pet->happiness = 0;
	}
}

void onButtonPress(uint8_t buttons) {
	if (buttons & BUTTON_RIGHT)
		onGameRightPressed();
	else if (buttons & BUTTON_UP)
		onGameUpPressed();
	else if (buttons & BUTTON_DOWN)
		onGameDownPressed();
	else if (buttons & BUTTON_LEFT)
		onGameLeftPressed();
	else if (buttons & BUTTON_SELECT)
		onSelectPressed();
}

void onGameRightPressed()
{
	selectedAction = getNextNumberCycle(selectedAction, 1, 3);
	printAction(selectedAction);
}

void onGameLeftPressed()
{
	selectedAction = getPreviousNumberCycle(selectedAction, 1, 3);
	printAction(selectedAction);
}

void onGameUpPressed()
{
	setupMenu();
}

void onGameDownPressed(){}

void onSelectPressed()
{
	pet* p = &mainPet;

	switch (selectedAction)
	{
	case ACTION_FEED:
		if (isFeedValid(*p))
			feedPet(p);
		break;
	case ACTION_PLAY:
		if (isPlayValid(*p))
			playPet(p);
		break;
	case ACTION_GROW:
		if (isGrowValid(*p))
			growPet(p);
		break;
	}
}

bool isFeedValid(pet p) {
	return p.stage >= 1 && p.fullness <= 3 && !isPetMaxAge(p.petAge);
}

bool isPlayValid(pet p) {
	return p.stage >= 1 && p.happiness < 2 && p.fullness >= 2 && !isPetMaxAge(p.petAge);
}

bool isGrowValid(pet p) {
	return p.stage == 1 && getPetAgeInSeconds(p.petAge) >= 35 && p.happiness >= 1 && p.fullness >= 3 && !isPetMaxAge(p.petAge);
}

void feedPet(pet* p) {
	if (p->fullness < 3) {
		p->fullness++;
	}
	else if (p->fullness == 3) {
		p->fullness++;
		p->happiness = 0;
	}
	printStatus(p);
	printAction(selectedAction);
}

void playPet(pet* p) {
	p->happiness++;
	printStatus(p);
	printAction(selectedAction);
}

void growPet(pet* p) {
	p->stage = 2;
	printStatus(p);
	printAction(selectedAction);
}

void setupMenu() {
	lcd.setBacklight(GREEN);
	mode = MODE_MENU;
	isMenuConfirmYes = true;
	selectedOption = MENU_OPTION_SAVE;
	pauseAgeing();
	openMainMenuDialog();
}

void runMenu() {
	uint8_t buttons = lcd.readButtons();

	if (buttons)
	{
		if (!buttonPressed) {
			onMenuButtonPress(buttons);
		}
		buttonPressed = true;
	}
	else {
		buttonPressed = false;
	}
}

void onMenuButtonPress(uint8_t buttons) {
	if (buttons & BUTTON_RIGHT)
	{
		onMenuRightPressed();
	}
	else if (buttons & BUTTON_UP)
	{
		onMenuUpPressed();
	}
	else if (buttons & BUTTON_DOWN)
	{
		onMenuDownPressed();
	}
	else if (buttons & BUTTON_LEFT)
	{
		onMenuLeftPressed();
	}
	else if (buttons & BUTTON_SELECT)
	{
		onMenuSelectPressed();
	}
}

void onMenuRightPressed()
{
	if (menuState == MENU_STATE_MAIN) {
		selectedOption = getNextNumberCycle(selectedOption, 0, 3);
		printOption(selectedOption);
	}
	else {
		isMenuConfirmYes = !isMenuConfirmYes;
		selectMenuOption();
	}
	
}

void onMenuUpPressed()
{
	returnToGameplay();
}

void onMenuDownPressed()
{
	if (menuState == MENU_STATE_MAIN) {
		returnToGameplay();
	}
	else {
		openMainMenuDialog();
	}
}

void onMenuLeftPressed()
{
	if (menuState == MENU_STATE_MAIN) {
		selectedOption = getPreviousNumberCycle(selectedOption, 0, 3);
		printOption(selectedOption);
	}
	else {
		isMenuConfirmYes = !isMenuConfirmYes;
		selectMenuOption();
	}
	
}

void onMenuSelectPressed()
{
	if (menuState != MENU_STATE_MAIN && !isMenuConfirmYes) {
		openMainMenuDialog();
	}
	else {
		switch (menuState)
		{
		case MENU_STATE_MAIN:
			selectMenuOption();
			break;
		case MENU_STATE_SAVE:
			savePet(mainPet);
			break;
		case MENU_STATE_DELETE:
			deleteSave();
			break;
		case MENU_STATE_NEW:
			newPet();
			break;
		}
	}
	
}

void savePet(pet p) {
	eeprom_write_byte((uint8_t*)0, SAVE_PRESENT);
	eeprom_write_byte((uint8_t*)1, p.stage);
	eeprom_write_byte((uint8_t*)2, p.happiness);
	eeprom_write_byte((uint8_t*)3, p.fullness);
	eeprom_write_dword((uint32_t*)4, second(stoppedTime));
	eeprom_write_dword((uint32_t*)8, minute(stoppedTime));

	lcd.setCursor(0, 0);
	lcd.clear();
	lcd.print("SAVED.");
	lcd.setCursor(0, 1);
	lcd.print("RESETTING...");
	delay(2000);
	setup();

}

void newPet() {
	assignGlobals();
	setupMainGameplay(false);
}

void deleteSave() {
	eeprom_write_byte((uint8_t*)0, SAVE_NULL);
	lcd.setCursor(0, 0);
	lcd.clear();
	lcd.print("DELETED.");
	delay(2000);
	openMainMenuDialog();
}

void selectMenuOption() {
	switch (selectedOption)
	{
	case MENU_OPTION_SAVE:
		menuState = MENU_STATE_SAVE;
		openConfirmation("SAVE");
		break;
	case MENU_OPTION_NEW:
		menuState = MENU_STATE_NEW;
		openConfirmation("CREATE");
		break;
	case MENU_OPTION_DELETE:
		//'byte savePreset = eeprom_read_byte((uint8_t*)0)' caused a compiler error, not sure why.
		if (eeprom_read_byte((uint8_t*)0) == SAVE_PRESENT) {
			menuState = MENU_STATE_DELETE;
			openConfirmation("DELETE");
		}
		else {
			lcd.setCursor(0, 0);
			lcd.clear();
			lcd.print("NO SAVE PRESENT.");
			delay(2000);
			lcd.clear();
			openMainMenuDialog();
		}
		break;
	case MENU_OPTION_LEAVE:
		returnToGameplay();
		break;
	}
}

void returnToGameplay() {
	mode = MODE_GAMEPLAY;
	resumeAgeing();
	lcd.setBacklight(WHITE);
	lcd.clear();
	printStatus(&mainPet);
	printAction(selectedAction);
	
}

void printOption(char option) {
	lcd.setCursor(0, 1);
	
	switch (option)
	{
	case MENU_OPTION_SAVE:
		lcd.print(ACTION_VALID);
		lcd.setCursor(2, 1);
		lcd.print("SAVE       ");
		break;
	case MENU_OPTION_NEW:
		lcd.print(ACTION_VALID);
		lcd.setCursor(2, 1);
		lcd.print("NEW PET    ");
		break;
	case MENU_OPTION_DELETE:
		if (eeprom_read_byte((uint8_t*)0) == SAVE_PRESENT) {
			lcd.print(ACTION_VALID);
		}
		else {
			lcd.print(ACTION_INVALID);
		}
		lcd.setCursor(2, 1);
		lcd.print("DELETE SAVE");
		break;
	case MENU_OPTION_LEAVE:
		lcd.print(ACTION_VALID);
		lcd.setCursor(2, 1);
		lcd.print("LEAVE MENU ");
		break;
	}
}

void openMainMenuDialog() {
	lcd.clear();
	menuState = MENU_STATE_MAIN;
	isMenuConfirmYes = true;
	lcd.clear();
	lcd.setCursor(0, 0);
	lcd.print("MENU");
	printTime(stoppedTime, 11);
	printOption(selectedOption);
}

void printTime(time_t t, char pos) {
	lcd.setCursor(pos, 0);
	if (minute(t) < 10) {
		lcd.print("0");
		lcd.print(minute(t));
	}
	else {
		lcd.print(minute(t));
	}
	lcd.print(":");
	if (second(t) < 10) {
		lcd.print("0");
		lcd.print(second(t));
	}
	else {
		lcd.print(second(t));
	}
}

void openConfirmation(String type) {
	lcd.setCursor(0, 0);
	lcd.print("ARE YOU SURE?   ");
	printYesNo(type);
}

void printYesNo(String type) {
	lcd.setCursor(0, 1);
	if (isMenuConfirmYes) {
		lcd.print("> YES, ");
		lcd.print(type);
	}
	else {
		lcd.print("> NO, GO BACK   ");
	}
}


char getNextNumberCycle(char num, char min, char max) {
	if (num < max) {
		return ++num;
	}
	return min;
}

char getPreviousNumberCycle(char num, char min, char max) {
	if (num > min) {
		return --num;
	}
	return max;
}

void pauseAgeing() {
	setTime(0, minute(), second(), 0, 0, 0);
	stoppedTime = now();
}

void resumeAgeing() {
	setTime(stoppedTime);
}






