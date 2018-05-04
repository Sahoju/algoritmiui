# Käyttöliittymä lajittelualgoritmien tehokkuuden mittaamiselle

## Suunnitellut toiminnot

* Tekstikenttä käyttäjän syöttämälle taulukon koolle
* Lajittelualgoritmin valinta
* Enemmän kuin kaksi lajittelualgoritmia
* Lajittelualgoritmin käynnistys
* Kuvio, josta näkyy lajittelualgoritmin tehokkuus (yksi lajittelu -> yksi piste koordinaatistoon, muodostaen suoran)
* Koordinaatiston sivuille tekstikentät, joissa näkyy niiden kohtien arvot
* x- ja y-akselien yksiköiden vaihto (taulukon koko, kierrosten määrä, aika, prosessorin käyttö, muistin käyttö)
* Tuloksien tallennus tietokantaan
* Välilehti, jossa voi verrata tietokantaan tallennettuja tuloksia

## Toteutetut toiminnot

* Tekstikenttä käyttäjän syöttämälle taulukon koolle
* Lajittelualgoritmin valinta
* Lajittelualgoritmin käynnistys
* Kuvio, josta näkyy lajittelualgoritmin tehokkuus (yksi lajittelu -> monta pistettä koordinaatistoon, muodostaen käyrän)
* Koordinaatiston sivuille tekstikentät, joissa näkyy niiden kohtien arvot
* Tekstikentät ajosta saadusta datasta
* Koordinaatiston tyhjennys
* Kursorin vietäessä kuvion pisteen päälle tulee esille sen pisteen data (kierrosten määrä ja aika siinä vaiheessa ajoa)
* Virheiden käsittely

## Toteuttamatta jääneet toiminnot

* Enemmän kuin kaksi lajittelualgoritmia
* x- ja y-akselien yksiköiden vaihto (taulukon koko, kierrosten määrä, aika, prosessorin käyttö, muistin käyttö)
* Tuloksien tallennus tietokantaan
* Välilehti, jossa voi verrata tietokantaan tallennettuja tuloksia

## Versio 0.1

![Versio 0.1](/01.PNG)

## Versio 0.2 (nykyinen)

### Alkunäkymä

![Alkunäkymä](/01_alkunakyma.PNG)

### Piirretty käyrä

![Piirretty käyrä](/02_kuvio.PNG)

### Monta käyrää

![Monta käyrää](/03_monta_kuviota.PNG)

## Ongelmat ja bugit

* Quick sortattu kuvio ei näytä oikealta
* Käyrä ei muutenkaan näytä ollenkaan samalta, kuin mitä muissa...

## Pohdinta

Harjoitustyön teko alkoi kivisesti. Tietorakenteet ja algoritmit -kurssin harjoitustöiden esitykseen oli noin viisi päivää, eikä työtä oltu saatu aluille. Päivät siis kuluivat epämiellyttävissä oloissa, kun vaikka aikaa oli vähän, silti työstä haluttiin saada hyvä. Käyttöliittymien ohjelmoinnin -kurssin puitteissa tämän myötä työ oli kuitenkin saatu erittäin hyvälle vaiheelle hyvissä ajoin. Silti pitää olla unohtamatta koettua oppia: Töitä ei pidä jättää viime tinkaan, vaikka kuvittelee tuotoksen tulevan olemaan miten yksinkertainen tahansa. Ohjelmoinnissa on paljon ulottuvuuksia ja kaikki ne pitää ottaa huomioon, mistä syystä yksinkertaiseen työhön voi upota yllättävän pitkä aika.

Ennen työn aloitusta ohjelmasta tehtiin nopeasti yksinkertainen mock-up. Se oli ainoa suunnitelma ohjelmalle, ja tämä aiheutti ongelmia algoritmikurssin jälkeen tehdyssä työssä. Melkein kaikki koodi tuli tehtyä lennosta, eikä tullut paljoa mietittyä, miten jotkut nykyiset toteutukset voivat aiheuttaa vaikeuksia muiden ominaisuuksien lisäämiseen. Siksi koodia piti monta kertaa kirjoittaa uudestaan. Se vei paljon aikaa, ja siksi lopputuloksena on vieläkin vaiheessa oleva tekele.

Opittua tuli valtavasti monelta puolin. Yksinkertaiset, "kuivat" ohjelmatkin voivat sisältää paljon ominaisuuksia, joita käyttäjänä ei tule otettua huomioon tai tulee pidettyä itsestäänselvänä. Etukäteen suunnitteleminen on tärkeää juuri siksi, että näitä pikkuseikkoja osataan ottaa huomioon etukäteen, eikä tarvitse kesken koodauksen havahtua jonkin ominaisuuden tarpeeseen. Kuten Jani sanoi: "Huonokin suunnitelma on tyhjää parempi." Vaikka lopputuotos meni reisille, olen silti todella innoissani kaikesta tästä uudesta tiedosta enkä malta odottaa päästäkseni hyödyntämään sitä!

## Arviointi

Koska kyseessä on vaiheessa oleva työ, en uskalla vaatia kolmosta (3) suurempaa arvosanaa. Tekisi kovasti mieli pyytää nelosta (4) kaiken tämän ajan ja vaivan jälkeen, mutta eihän uudelleen kirjoitettu koodi näy lopputuloksessa.

(lisäksi en ollenkaan katsonut arviointikriteereitä työskentelyn aikana. hups!)
