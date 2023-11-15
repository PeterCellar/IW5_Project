# Zadání - Webová aplikace pro donášku jídel

Výsledná aplikace má sloužit jako jednoduchá aplikace simulující webovou stránku pro donášku jídel.

---

## Data

V rámci dat, se kterými se bude pracovat budeme požadovat minimálně následující data.

### Restaurace

- Název
- Logo
- Textový popis
- Adresa
- Souřadnice na mapě

### Jídlo

- Název
- Fotografie
- Textový popis
- Cena
- Seznam alergenů

### Objednávka

- Jméno
- Adresa
- Požadovaný čas doručení
- Poznámka
- Stav objednávky
- Seznam a počet objednaných jídel i s možnými poznámkami

---

## Funkcionalita

Webová aplikace bude obsahovat několik stránek pro zobrazování a zadávání dat.

V zadání není požadováno perzistentní uložení dat. To znamená, že když se aplikace restartuje, tak může o data přijít. Nicméně bude nutno data ukládat za běhu aplikace, aby bylo možno demonstrovat, že když se například pomocí aplikace přidá nový záznam, tak se tento zobrazí v příslušném seznamu záznamů, dá se editovat, smazat atd.

Minimální rozsah, který je požadován v rámci projektu je popsán v této kapitole.

### Seznam restaurací

Seznam bude obsahovat všechny restaurace dostupné v aplikaci. Bude možno se z něj překliknout na detail restaurace a na pohled pro přidání nové restaurace.

### Detail restaurace

Zobrazuje detail restaurace se všemi informacemi o něm a se seznamem jídel.

### Editace restaurace

Stránka, která slouží na editaci restaurace. Může se využít na vytvoření nové restaurace nebo na editaci existující. Bude obsahovat všechny informace o restauraci.

### Seznam jídel

Seznam jídel v restauraci. Bude možno se překliknout na detail jídla a přidání nového jídla. Jídla se budou dát řadit minimálně dle ceny. A budou se dát filtrovat minimálně pomocí alergenů.

### Detail jídla

Zobrazuje detail jídla se všemi informacemi o něm.

### Editace jídla

Stránka, která slouží na editaci jídla. Může se využít na vytvoření nového jídla nebo na editaci existujícího. Bude obsahovat všechny informace o jídle.

### Seznam objednávek

Pohled obsahuje všechny objednávky v rámci systému. Bude možno se z něj překliknout na detail objednávky a na pohled pro přidání nové objednávky.

### Detail objednávky

Stránka zobrazuje všechny informace o konkrétní objednávce včetně jejího stavu.

### Editace objednávky

Stránka, která slouží na editaci objednávky. Může se využít na vytvoření nové objednávky nebo na editaci existující. Bude obsahovat všechny informace o objednávce.

### Stránka "Tržby restaurace"

Stránka zobrazí tržby pro vybranou restauraci - t.j. sumu z objednávek, které byly úspěšně doručeny a zaplaceny.

### Stránka "Vyhledávání"

Stránka, na které můžete použít textové vyhledávání napříč záznamy v aplikaci. Seznam všech nalezených záznamů se zobrazí na stránce a bude se dát překlikem dostat na detail daného záznamu (tedy například v případě týmu se odnaviguje na detail týmu). Textově se vyhledává minimálně v těchto atributech:

- Restaurace
  - Název
  - Textový popis
  - Adresa
- Jídlo
  - Název
  - Textový popis

---

### Fáze 1 – API

V první fázi se zaměříme na vytvoření Web API služby. Výstupem tedy bude spustitelný projekt, který obsahuje Web API, poskytuje specifikaci ve standardu OpenAPI (výběr verze necháme na vás) a poskytuje přístup k API pomocí Swagger inspektoru. API obsahuje minimálně metody pro:

- Restaurace
  - Získání seznamu restaurací
  - Získání detailu restaurace
  - Vytvoření nové restaurace
  - Upravení existující restaurace
  - Smazání existující restaurace
- Jídlo
  - Získání seznamu všech jídel pro restauraci
  - Získání detailu jídla
  - Vytvoření nového jídla
  - Upravení existujícího jídla
  - Smazání existujícího jídla
- Objednávka
  - Získání seznamu všech objednávek pro restauraci
  - Získání detailu objednávky
  - Vytvoření nové objednávky
  - Upravení existující objednávky
  - Smazání existující objednávky
- Tržby
  - Získání seznamu tržeb pro jednotlivé restaurace
- Vyhledávání
  - Získání výsledků vyhledávání
    Vzorové API, dle kterého se můžete inspirovat bude ukazováno na přednáškách/cvičeních.

V 1. fázi bude také požadováno pokrytí API testy. Minimálně musí být pokryty všechny API endpointy dostatečným počtem testů, aby se pomocí nich dala ověřit správnost funkcionality API.

Počítáme tedy s tím, že budete mít vytvořeny testy, které můžeme spustit jak lokálně tak v rámci Azure DevOps a tyto testy testují správnost Vašeho řešení. To, jak psát testy bude ukázáno v rámci přednášek/cvičení.

Budeme tedy kontrolovat jak to, že máte napsány správné testy tak to, že aplikace funguje.

Hodnotíme:

- logický návrh tříd
- splnění funkcionality
- využití abstrakce, zapouzdření, polymorfismu
- čistotu kódu
- verzování v GITu po logických částech
- testy
- automatizované nasazení do Azure (CI + CD) z Azure DevOps
- logické rozšíření datového návrhu nad rámec zadání (bonusové body)

---

### Fáze 2 - Web

V druhé fázi se od vás bude požadovat vytvoření webové aplikace pomocí technologie Blazor. Webová aplikace bude napojena na API vytvořeno v první fázy projektu.

Hodnotíme:

- opravení chyb a zapracování připomínek, které jsme vám dali v rámci hodnocení fáze 1
- funkčnost celé výsledné aplikace
- zobrazení jednotlivých informací dle zadání – seznam, detail, vytváření, editace, mazání…
- čistotu kódu
- vytvoření dobře vypadající aplikace (bonusové body)
