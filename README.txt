Code available: https://github.com/kamilkozinski/GameC
1.10 New monster classes
	1. Ghost Class - monster2140, ghost has 50% to dodge player's attack 
	2. Dragon Class - monster2141, ordinary dragon, deals 2 different attacks depending on stamina level, has ability to regenerate stamina
	3. SilverDragon Class - monster2142, it's 60% chance that silver dragon will be spawned after defeating green(ordinary) dragon. Silver dragon uses 3 different attacks, one depending on MagicPower level 
	4. GoldenDragon Class - monster2143, it's 17% chance that golden dragon will be spawned afted defeating silver dragon. 
1.11 New monster factories
	1. DragonFactory Class - allows to spawn dragons
	2. GhostFactory Class - allows to spawn ghosts
1.2 New item classes
	1. BerserkerBoots Class - similar to BerserkerArmor
	2. BerserkerGloves Class - similar to BerserkerArmor
	Combined together with BerserkerArmor, those items allow player to create Berserker Set with extra Stamina bonus (2 items: 10 additional Stamina for every piece of set, 3 items: 15 additional Stamina for every piece of set)
	3. Aerondight Class - new sword in game that decreases opponent armor
1.21 New item factories
	1. SwordFactory Class- allows to spawn swords(e.g Aerondight)
1.3 New skills classes
	1. Whirl Class - allows to use Sword Whirl - 3 super fast incised attacks
	2. FastAttack Class - allows to use Sword Fast Attack ability, attacking fast but with low dmg and stamina consumption
	3. StrongAttack Class - allows to use Sword Strong Attack ability, attacking with great dmg and high stamina consumption
	3. WhirlDecorator Class - allows to create COMBO with other attacks
	4. FastAttackDecorator Class - allows to create COMBO with other attacks
	5. StrongAttackDecorator Class - allows to create COMBO with other attacks
1.31 New skills factoreis
	1. SwordPlayFactory Class - allows to produce new attack objects and creating COMBO's
~~~~~~
1.4 New interactions
	1. QuestDoor1 Class - gives player quests to kill monsters that are already spawned on map
	2. QuestDoor2 Class - available only after finishing quests from QuestDoor1
	3. Chest Class - available only after finishing quests from QuestDoor2
	3. IQD1Strategy - abstract class that determine different strategies for QuestDoor quests:
		3.1 QD1FirstQuestStartStrategy
		3.2 QD1SecondQuestStartStrategy
		3.3 QD1ThirdQuestStartStrategy
		3.4 QD1BetweenTasksStrategy
		3.5 QD2DefaultStrategy
		3.6 QD2FourthQuestStartStrategy
	4. IBattleStrategy interface that determines different strategies for class Battle
		4.1 Q1Strategy
		4.2 Q2Strategy
		4.3 Q3Strategy
	5. QuestDoorFactory Class - abstract factory for quests QuestDoor1 and QuestDoor2
	6. KilledMonstersCounter Class - implements Singleton design pattern and allows to track number of killed monsters during tasks and provide communication between GameSession and QuestDoor classes
	7. IMediator - interface connected with QuestsDoorMediator
	8. QuestDoorMediator Class - provide communication between KilledMonstersCounter, QuestDoor1 and QuestDoor2 classes
	9. Key Class - item to chest