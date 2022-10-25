# FEJLESZTÉSI TERV

A fejlesztési folyamat inkrementálishoz hasonló módon történik, azaz minden alkalommal egy új funkcióval bővül a szoftver. Összesen 6 konzultációs alkalom lesz, tehát én eszerint 6 lépésre osztottam a fejlesztési folyamatot:
1. A szoftver struktúrájának, architektúrájának és főbb osztályainak megtervezése (UML osztálydiagram).
2. A játékos tudja mozgatni az avatárt, és vele mozog a kamera. Alapvető inputfeldolgozás és megjelenítés.Ellenségek bevezetése a játékba, akiket meg lehet ölni, de ők nem tudják megtámadni a játékost. A játékosnak van egy kardja, amivel meg tudja támadni az ellenségeket.
3. Az ellenség tud támadni, és meg tudja ölni a játékos avatárját. Ha az avatár meghal, akkor egy gomb jelenik meg, amit megnyomva újraéled.
4. Tapasztalati pontok, képességek és szintek bevezetése a játékba. Minden szinten egyre több tapasztalati pont kell a következőhöz. Az ellenségek megöléséért már tapasztalati pont is jár.Alapvető menü bevezetése.
5. Leltárrendszer, további tárgyak és az arany bevezetése a játékba. A játékos avatárja több tárgyat is magánál tarthat, ezeket akár felszerelheti, vagy elpusztíthatja. A leltár egy ablakban jelenik meg, ahol látszanak a tárgyak, és a lehetséges opciókra van egy-egy gomb. A játékos az ellenségei megöléséért kaphat aranyat és/vagy tárgyat. Az újabb képességek kitanulására van egy felület, ahol a képességre kattintva lehet azt megtanulni, ha az előfeltételek teljesülnek.
6. Világgenerálás és/vagy világ létrehozására szükséges felület. A világ tartalmaz:
    - Ellenség spawner-eket, amik újabb ellenségeket hoznak létre időközönként.
    - Blokkokat, amik olyan területek a világban, amiken nem lehet átmenni
    - Boltokat, ahol különböző tárgyakat lehet venni
    
