# AutoSkelbiu

**Vilniaus universitetas  
Informacinių sistemų inžinerija  
.NET programavimo projektas  2021m.**

<img src="https://user-images.githubusercontent.com/60687269/156175638-3171b17e-035f-49f9-9a83-fee0dd9ff989.png" width="250px" />

**AutoSkelbiu** yra didžiausių automobilių skelbimų svetainių sujungimas į vieną sistemą. Projektas buvo naudojamas **EDUKACINIAIS TIKSLAIS** ir **NIEKADA** nebuvo naudojamas komerciniais tikslais. Visi atvaizduojami duomenys, kurie buvo gauti iš automobilių skelbimų svetainių visais atvejais nukreipdavo į originalų autorių. Gautuose duomenyse nebuvo informacijos apie asmenį t.y. pardavėją (tel.nr., vardas, t.t.), o buvo gauti viešai prieinami ir teisių nepažeidžiantys duomenys kaip automobilio specifikacijos, kaina, nuoroda nukreipianti į originalų skelbimą ir t.t., todėl šis projektas nepažeidžia asmens duomenų privatumo teisės ir nesukelia nuostolių automobilių skelbimų svetainėms.

### Projektas sudarytas iš:
* Vue js frontend aplikacijos. (Visų skelbimų atvaizdavimas)
* .NET 5 web api backend aplikacijos (Tarpininkas tarp duomenų bazės ir jų pateikimo frontend, prisijungimai prie MySQL duomenų bazės buvo panaikinti projekto įkelimo metu).
* Autoplius scraperio (skelbimo info), .net console application.
* Autogidas scraperio (skelbimo info), .net console application.

### Papildomos programos, kurios nėra įtrauktos į repozitoriją:
* MySql duomenų bazė
* Autoplius scraperio, (skelbimų url, kuris įrašomas į db ir naudojamas skelbimo info scraperio) .net console application.
* Autogidas scraperio, (skelbimų url, kuris įrašomas į db ir naudojamas skelbimo info scraperio) .net console application.
* Autoplius ir autogidas VIN scraperiai, (naudojantis JS)  .net windows application naudojantis cefsharp.
