# Temporary ponglish version


# Map Generator
> Unity tool for procedural generation of two-dimensional maps.

![License](https://img.shields.io/github/license/TukanHan/Map-Generator?style=flat-square)
![LastCommit](https://img.shields.io/github/last-commit/TukanHan/Map-Generator?style=flat-square)
![Language](https://img.shields.io/github/languages/top/TukanHan/Map-Generator?style=flat-square)

## ReadMe content

* [Description](#description)
* [Map examples](#map-examples)
* [Usage](#usage)
	* [Editor](#editor)
	* [Data Models](#data-models)
* [License](#license)

## Description
### About tool
Narzędzie umożliwia generowanie map zarówno w trybie projektowania jak i przy starcie sceny. Posiada edytor pozwalający na parametryzację generatora i dodawanie nowych modeli danych bez konieczności programowania. Parametryzacja generatora daje duży wpływ na efekt finalny wygenerowanej mapy. Umożliwia wygenerowanie mapy z podanego ziarna losowości, przez co dla tych samych parametrów możliwe jest odwzorowanie tej samej mapy. 

### About map generation
Działanie generatora opiera się na mapach szumu. W pierwszej kolejności generowane są mapy szumu wysokości i temperatury. Dla każdej komórki na mapie, na podstawie wartości z tych dwóch map wyliczany jest poszczególny biom z przygotowanego diagramu biomów. Dzięki temu przechodzenie między biomami na mapie wygląda naturalnie, a zarazem różnorodnie. Na tak wygenerowaną mapę składającą się z biomów, generowana jest warstwa wody, utworzona również za pomocą mapy szumu. Na sam koniec generowane są obiekty na mapie, takie jak: lokacje, roślinność i inne obiekty, przy czym każdy biom posiada własne obiekty, które będą na nim generowane.

## Map examples
Poniżej znajduje się kilka przykładów wygenerowanych map dla odmiennych parametrów generatora. Narzędzie umożliwia dodawanie nowych obiektów, definiowanie własnych modeli biomów i lokacji, podmianę istniejącej grafiki lub wprowadzenie własnego stylu graficznego. Zachęcam do eksperymentów i tworzenia własnych światów.

> Kliknij na konkretny obrazek, aby zobaczyć go w powiększeniu.

<img src="https://drive.google.com/uc?id=1jHI-vV0bvMqIG-8VD23mnXE_IqIH4ziO" alt="alt text" width="49%" height="49%"> <img src="https://drive.google.com/uc?id=1vbHfKjriRNOAG9IQs1gB80p_DFB2SYQ_" alt="alt text" width="49%" height="49%">
<img src="https://drive.google.com/uc?id=1mVKpooJsbKvZbkBvN2ZzJhDv3Hi6hPNz" alt="alt text" width="49%" height="49%"> <img src="https://drive.google.com/uc?id=1_LYgqZBkFCJcdRAxLZNt1fNThZy2F2nn" alt="alt text" width="49%" height="49%">
<img src="https://drive.google.com/uc?id=1yNUibrpAFfxmL5uqKPX_YZ9nWCVuzzXW" alt="alt text" width="49%" height="49%">  <img src="https://drive.google.com/uc?id=1LU7cpGLaM8-WbzsKudsyPtzm-KztNQWB" alt="alt text" width="49%" height="49%">

## Usage

Narzędzie zostało zaprojektowane aby było proste w obsłudze i każdy był w stanie je użytkować bez potrzeby edycji kodu źródłowego. Na narzędzie składają się: edytor generatora, który pozwala parametryzować generowane mapy oraz modele danych, które reprezentują konkretne obiekty jak: biomy, lokalizacje czy pojedyńcze obiekty.

### Editor

<img src="https://drive.google.com/uc?id=1WzwunozTFa08FIU43Sk7iEg8ay7Dfc4v" alt="Editor" width="70%" height="70%">

#### Size section
Narzędzie umożliwia generowanie map w przestrzeni dwuwymiarowej. W tej sekcji można ustalić wymiary wygenerowanej mapy.

#### Height & Temperature noise map section
W tych sekcjach można modyfikować parametry map szumów temperatur i wysokości. Od wygenenerowanych wartości z tych dwóch map szumów zależeć będzie jakie biomy zostaną przypisane do konkretnych komórek na mapie.
>**Oktawy** wpływa na to jak bardzo rozmyte będą sąsiadujące strefy biomów. Czym większa wartość, tym bardziej będą one poszarpane.

>**Ziarnistość** wpływa na to jak zróżnicowane będą wygenerowane biomy. Im niższa wartość, tym bardziej będą one zróżnicowane. 

>**Zakres wartości** umożliwiają ograniczenie wygenerowanych wartości map szumów do podanego zakresu. Umożliwia to wygenerowanie na mapie tylko określonych biomów z całego spektrum.

>**Target Value** umożliwia przesunięcie generowanych wartości w którąś ze stron spektrum, w taki sposób, aby pewne wartości pojawiały się częściej.

#### Water noise map section
W tej sekcji można modyfikować parametry mapy szumu wody. Posiada parametry jak sekcje wyżej. Od wygenerowanych tu wartości zależy jak będzie wyglądać warstwa wody na mapie.

>**Water area percent** wpłyna na minimalną i maksymalną procentową wartość wody na mapie.

#### Water biomes section
W tej sekcji można definiować kolejne warstwy głębokości wody na mapie. Umożliwia to uworzenie osobnych warstw dla wody płytkiej, głębokiej lub nawet zasymulowanie wysp na wodzie.

>**Water Thresholding** definiuje próg dla wartości z mapy szumu wody po przekroczeniu którego, zostanie wybrana ta warstwa. Każda kolejna warstwa wody musi mieć większą wartość progu.

>**Biom** przypisany tu model biomu będzie reprezentował daną warstwę wody.

#### Biomes diagram section
W tej sekcji można defioniwać diagram biomów. Ustalać jego rozmiar oraz przypisywać modele biomów do odpowiednich komórek w diagramie. Od przypisanych tu wartości zależy jakie biomy na mapie będą ze sobą sąsiadować. Biomy na mapie wybierane są z diagramu biomu na podstawie wartości z map szumów wysokości i temperatury.

#### Generation section
W tej sekcji można ustalić szczegóły dotyczące generacji mapy oraz wygenerować ją zapośrednictwem przycisku.

>**Generation type** pozwala wybrać czy obiekty mają być generowane na Tile Mapie, czy może w postaci spritów.

>**Generate on start** pozwala wybrać czy mapa powinna być generowana przy starcie sceny.

>**Generate random seed** pozwala wybrać czy mapa ma zostać wygenerowana na podstawie losowego zarna, czy ma on zostać wprowadzone.

>**Seed** pozwala wprowadzić konkretne ziarno losowości, dzięki któremu dla tych samych parametórw można odwzorować identyczną mapę.

### Data models
Modele danych służą do przechodwywania informacji na których operuje generator map. Są one zapisywane w Unity za pomocą mechanizmu Scriptable Object, dzięki temu jeden przygotowany model danych może być przypisany do wielu miejsc. Narzędzie posiada kilka typów modeli danych, które można przedstawić na schemacie hierarchicznym.

<p align="center"><img src="https://drive.google.com/uc?id=13OJpWmn0sEq7mBngUwIS9LRb_ZPX3Bu-" alt="Data Models" width="75%" height="75%"></p>

Aby utworzyć nowy model danych w projekcie Unity należy wcisnąć PPM w widoku projektu, z menu kontekstowego wybrać Create -> Map Generator, a następnie konkretny typ modelu danych. W zamieszczonym projekcie można zobaczyć przykłady zastosowań takich modeli danych.

>**Intensity** określa szansę wygenerowania obiektu dla każdej pojedyńczej komórki na mapie, o ile pozwalają na to warunki. Nie określa ono pokrywania samej mapy danym typem obiektów.

>**Prioryty** określa szansę na wylosowanie tego elementu z całej kolekcji obiektów. Im większy priorytet tym większa szansa. Do wylosowania obiektu z kolekcji używany jest mechanizm selekcji koła ruletki.

>**Max Count** określa maksymalną możliwą liczbę wystąpień danego obiektu w ramach obiektu rodzica.

## License 
Autorem tego narzędzia jest **TukanHan** i jest ono udostępnione na licencji <a href="http://badges.mit-license.org" target="_blank">**MIT**</a>. Masz przyzwolenie na robienie z nim czego chesz bez pytania mnie o zgodę, jednak jeśli doceniasz mój wkład proszę o uznanie autorstwa. 

Jeśli podoba Ci się ten projekt wesprzyj moją pracę udostępniając projekt innym urzytkownikom, aby oni też mogli z niego skorzystać.
