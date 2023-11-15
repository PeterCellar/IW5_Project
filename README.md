# Webová aplikácia pre donášku jedál

Aplikácia slúži ako jednoduchá aplikácia simulujúca webovú stránku pre donášku jedál.

Aplikácia bola vytvorená ako školský projekt na FIT VUT mnou a ďalšími dvomi študentmi.

---

## Dáta

### Reštaurácia

- Názov
- Logo
- Textový popis
- Adresa
- Súradnice na mape

### Jedlo

- Názov
- Fotografie
- Textový popis
- Cena
- Zoznam alergénov

### Objednávka

- Meno
- Adresa
- Požadovaný čas doručenia
- Poznámka
- Stav objednávky
- Zoznam a počet objednaných jedál aj s možnými poznámkami

---

## Funkcionalita

Webová aplikácia obsahuje niekoľko stránok pre zobrazovanie a zadávanie dát.

V riešení nie je perzistentné uloženie dát. 

### Zoznam reštaurácií

Zoznam obsahuje všetky reštaurácie dostupné v aplikácii. Dá sa z neho prekliknúť na detail reštaurácie a na pohľad na pridanie novej reštaurácie.

### Detail reštaurácie

Zobrazuje detail reštaurácie so všetkými informáciami o ňom a so zoznamom jedál.

### Editácia reštaurácie

Stránka, ktorá slúži na editáciu reštaurácie. Môže sa využiť na vytvorenie novej reštaurácie alebo na editáciu existujúcej. Obsahuje všetky informácie o reštaurácii.

### Zoznam jedál

Zoznam jedál v reštaurácii. Dá sa prekliknúť na detail jedla a pridanie nového jedla. Jedlá sa dajú radiť podľa ceny. Dajú sa filtrovať pomocou alergénov.

### Detail jedla

Zobrazuje detail jedla so všetkými informáciami o ňom.

### Editácia jedla

Stránka, ktorá slúži na editáciu jedla. Môže sa využiť na vytvorenie nového jedla alebo na editáciu existujúceho. Obsahuje všetky informácie o jedle.

## Zoznam objednávok

Pohľad obsahuje všetky objednávky v rámci systému. Dá sa z neho prekliknúť na detail objednávky a na pohľad na pridanie novej objednávky.

### Detail objednávky

Stránka zobrazuje všetky informácie o konkrétnej objednávke vrátane jej stavu.

### Editácia objednávky

Stránka, ktorá slúži na editáciu objednávky. Môže sa využiť na vytvorenie novej objednávky alebo na editáciu existujúcej. Obsahuje všetky informácie o objednávke.

### Stránka "Tržby reštaurácie"

Stránka zobrazí tržby pre vybranú reštauráciu - tj sumu z objednávok, ktoré boli úspešne doručené a zaplatené.

### Stránka "Vyhľadávanie"

Stránka, na ktorej môžete použiť textové vyhľadávanie naprieč záznamami v aplikácii. Zoznam všetkých nájdených záznamov sa zobrazí na stránke a dá sa preklikom dostať na detail daného záznamu. Textovo sa vyhľadáva v týchto atribútoch:

- Reštaurácia
  - Názov
  - Textový popis
  - Adresa
- Jedlo
  - Názov
  - Textový popis

---

### Web API

Poskytuje špecifikáciu v štandarde OpenAPI a poskytuje prístup k API pomocou Swagger inšpektora. API obsahuje metódy pre:

- Reštaurácia
  - Získanie zoznamu reštaurácií
  - Získanie detailu reštaurácie
  - Vytvorenie novej reštaurácie
  - Upravenie existujúcej reštaurácie
  - Zmazanie existujúcej reštaurácie
- Jedlo
  - Získanie zoznamu všetkých jedál pre reštauráciu
  - Získanie detailu jedla
  - Vytvorenie nového jedla
  - Upravenie existujúceho jedla
  - Zmazanie existujúceho jedla
- Objednávka
  - Získanie zoznamu všetkých objednávok pre reštauráciu
  - Získanie detailu objednávky
  - Vytvorenie novej objednávky
  - Upravenie existujúcej objednávky
  - Zmazanie existujúcej objednávky
- Tržby
  - Získanie zoznamu tržieb pre jednotlivé reštaurácie
- Vyhľadávanie
  - Získanie výsledkov vyhľadávania

API endpointy sú pokryté testami a sú spustiteľné aj v rámci Azure DevOps.

---

### Webová aplikácia

Webová aplikácia je vytvorená pomocou technológie Blazor. Webová aplikácia je napojená na Web API.

