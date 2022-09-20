# TÉMALABOR SPECIFIKÁCIÓ - GYÖRGYDEÁK LEVENTE, NC1O2T

## A Játék Alapműködése
A szoftver egy 2 dimenziós, felülnézetes kaland - és szerepjáték.

A felhasználó irányít egy avatárt/karaktert. A játék egy olyan világban játszódik, ami csak minimálisan korlátozza a játékos avatárját a mozgásban, ezért ez egy **open-world** játék. Az avatár képes szabadon mozogni 4 irányba (jobbra, balra, előre, hátra), de felfele és lefele nem. 

A játékos gyűjthet a világban **aranyat**, **tapasztalati pontot**, és **tárgyakat**.

## Ellenségek
A világban vannak ellenségek. Ha ezeket a játékos megöli, akkor kaphat jutalmakat (pl. aranyot, tapasztalati pontot, tárgyakat). Több fajta ellenség van:
- Távharci ellenség, akinek van egy hatótávolsága, amin belül megtámadja ellenségeit.
- Közelharci ellenség, ami az ellenségéhez odafut, és közelről támadja.


## Képességek

A játékos avatárjának vannak **képességei**. A képességek segítik a játékost az ellenségek legyőzésében, vagy valahogyan védelmezik a játékost. Képességek:
- Az avatár gyorsan egy kijelölt irányába ugrik.
- Az avatár előtt egy területrészen minden ellenséget megsebez.
- A játékos kiválaszt egy ellenséget, és az megsebződik, illetve pár másodpercig nem tud semmit cselekedni, azaz **kábítva** lesz.

A játékos avatárjának van egy szintje, ami az erősségét jelzi. Ezt a játékos fejlesztheti, amivel erősebbé válik, és képességeket tanulhat meg, vagy a meglévőket erősítheti. A tapasztalati pont segíti a játékost abban, hogy szintet fejlődjön.

## Attribútumok
Az játékos avatárjának és az ellenségeknek is van életereje, ami egy-egy számmal van reprezentálva. Ha az életerejük eléri a 0-t, vagy alá kerül, akkor meghalnak. Az avatár ezek után újraéled egy fix ponton, de tapasztalati pontot és aranyat veszít.

Az életerőn kívül még számos attribútumai lehetnek a játékos avatárjának, és az ellenségeknek:
- sebzés - ettől az értéktől (is) függ, hogy mennyivel csökkenti a célpont életerejét egy támadás hatására
- kritikus csapás esélye - egy kritikus találat többet sebez egy átlagos találatnál
- védekezés - ettől az értéktől (is) függ, hogy mennyivel csökken az avatár által elszenvedett életerő veszteség
- mozgási sebesség - ettől az értéktől (is) függ az avatár mozgási sebessége a világban

## Tárgyak
A világban van egy bolt, ahol a játékos tárgyakat tud venni, amik aranyba kerülnek. A tárgyak erősebbé teszik a játékost. Mindegyik tárgy különböző bónuszokat ad, különböző attribútumokat növelnek, vagy csökkentenek. Több fajta tárgy van:
- Fegyver. Befolyásolt attribútumok:
    - sebzés
    - kritikus csapás esélye
- Vért. Befolyásolt attribútumok:
    - védekezés
    - életerő
- Cipő. Befolyásolt attribútumok:
    - mozgási sebesség
    - védekezés
    - életerő

A játékos avatárjának van egy leltára, amiben több tárgyat is tárolhat, illetve felszerelhet. Nem szerelhet fel azonban 2 azonos típusú tárgyat (pl 2 különböző fegyvert).



## Menü
A játék elinduláskor egy menüt mutat a felhasználónak, amin az alábbi opciók érhetők el:
- Játék
    - Erre kattintva elindul a játék
- Kilépés
    - Erre kattintva bezárul a program

## Képernyőképek
**A játékos avatárja:**
![](avatar.png)
**A játékos avatárja egy karddal felszerelve:**
![](avatar-with-weapon.png)
**Közelharci ellenség, aki közeledik a játékos avatárja felé:**
![](enemy-attacking-avatar.png)