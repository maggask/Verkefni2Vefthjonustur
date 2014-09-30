Verkefni 2 í Vefþjónustum
=========================

Útfæra skal API fyrir einkunnir nemenda í kennslukerfi. Athugið að ekki er beðið um neinn framenda, heldur einungis um API aðgerðir. Þá er líka rétt að taka fram að verkefnið snýr eingöngu að einkunnum, en ekki að skilgreiningu verkefna, netprófa né annarra atriða sem komið geta við sögu í áfanga í kennslukerfi.

Ekki er gefið upp hvernig þessi API eiga að líta út, heldur er það hluti af verkefninu. Þó er reiknað með að viðkomandi API hafi að jafnaði forskeytið /api/courses/{courseInstanceID}.

"Functional" kröfur:

1. (20%) Kennari skal geta skilgreint einingahluti (sem geta verið af tegundunum verkefni, netpróf, miðannarpróf, lokapróf, endurtektarpróf), þ.e. hvernig einkunnir reiknast út, með eftirfarandi reglum:
	* Hægt skal vera að skrá "X af Y" bestu gilda
	* Hægt skal vera að skrá að tiltekinn verkhluti gildi til hækkunar (og þá hvaða einkunnahluti skuli gilda annars)
	* Hægt skal vera að skrá að tiltekinn hluti sé nauðsynlegur til að ná áfanganum (t.d. lokapróf)

2. (5%) Kennari skal geta skráð einkunnir í þeim einingahlutum sem hann hefur skilgreint í 1. 

3. (10%) Nemandi skal geta skoðað upplýsingar um eigin einkunnir. Hægt skal vera að fá upplýsingar um einstakar einkunnir í tilteknum einingahlutum. Þá skal nemandi geta fengið upplýsingar um lokaeinkunn sína, út frá reiknireglum sem kennari skilgreinir í 1. Þar skal taka fram hve mörg % af lokaeinkunn er tilbúið. Þá skal í báðum tilfellum (þ.e. bæði fyrir einkunnir í einstökum einkunnahlutum og í lokaeinkunnum) , vera hægt að sjá hvar í röðinni nemandi er samanborið við aðra nemendur í áfanganum.

4. (5%) Lausninni skal fylgja skjölun sem sýnir hvaða aðgerðir eru í boði í kerfinu, og leyfir að kalla í þær.

5. (10%) Kennari skal geta séð yfirlit yfir einkunnir allra nemenda, bæði einstaka einkunnahluta sem og lokaeinkunn.

Tæknilegar kröfur:

* (5%) Lagskipt högun
* (10%) Kerfið er með input validation og skilar lógískum HTTP error kóðum ásamt villuboðum ef eitthvað fer úrskeiðis.
* (15%) Einingaprófanir
* (10%) Kerfið skal passa að aðgerðir séu aðeins aðgengilegar notendum með réttindi til að framkvæma viðkomandi aðgerð. Þannig skulu nemendur ekki hafa aðgang að því að skrá einkunnir, og nemandi á aðeins að hafa aðgang að sínum eigin einkunnum en ekki annarra.
* (10%) Hönnun á API samrýmist RESTful hugmyndafræði

Eftirfarandi reglur skulu hafðar í heiðri:

* Samanlagt verður vægi einkunnahluta að vera 100 til að hægt sé að nota þá til að reikna út lokaeinkunn. Þessu skal framfylgja þegar kennari skráir hvernig lokaeinkunn er samsett.
* Lokaeinkunn nemanda getur ekki orðið hærri en 10.
* Lokaeinkunn skal gefa upp í heilum og hálfum.
* Einkunnir fyrir einstaka einkunnahluta geta verið hærri en 10, og þurfa ekki að takmarka sig við heilan og hálfan.

Sérstakur bónus fæst fyrir verkefnið ef allar kröfur eru uppfylltar, og kerfið býður jafnframt upp á lokaeinkunnir sem eru ekki tölur á bilinu 0 - 10, eins og "Staðið", "Metið".

Nemendur munu hafa aðgang að Auth þjóni sem verður hægt að tengjast til að fá token sem skal svo senda með öllum HTTP fyrirspurnum á API-ið (sjá fyrirlestur). Á þjóninum verða allir nemendur þessa áfanga skráðir, ásamt kennurum. Notendanöfn verða þau sömu og HR notendanöfn, og lykilorð verða þau sömu og notendanöfn (þetta er svona til að geta prófað að logga sig inn sem fleiri en einn nemandi, og sem kennari).
