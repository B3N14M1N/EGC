# Răspunsuri laboratorul 3-4
## 1. Care este ordinea de desenare a vertexurilor pentru aceste metode (orar sau anti-orar)? Desenați axele de coordonate din aplicația-template folosind un singur apel `GL.Begin()`.
    Ordinea de desenare a vertexurilor este in sens orar.
## 2. Ce este *anti-aliasing*? Prezentați această tehnică pe scurt.
  *Anti-aliasing* este o tehnică utilizată în grafică computerizată pentru a reduce sau elimina efectul de "*aliasing*", care este apariția pixelată a liniilor sau marginilor obiectelor pe ecran. Acest efect apare atunci când rezoluția ecranului nu este suficient de mare pentru a reda liniile și marginile obiectelor în mod clar, sau atunci când obiectele sunt poziționate într-un mod care creează contrast brusc între ele și fundal (înclinate oblic). 
  Anti-aliasing funcționează prin adăugarea de eșantionare suplimentară sau "mostre" între pixeli pentru a calcula culoarea intermediară. Aceste mostre sunt apoi combinate pentru a crea o imagine finală cu margini mai netede și mai naturale.
## 3. Care este efectul rulării comenzii `GL.LineWidth(float)`? Dar pentru `GL.PointSize(float)`? Funcționează în interiorul unei zone `GL.Begin()`?
  Comanda `GL.LineWidth(float)` din *OpenGL* setează grosimea liniilor desenate. Această comandă nu operează în interiorul unei secțiuni `GL.Begin()`. Efectul său este vizibil în întreaga aplicație *OpenGL* după apelarea acestei comenzi, până când este schimbată din nou cu o altă valoare de grosime a liniei.
  Comanda `GL.PointSize(float)` setează dimensiunea punctelor desenate în *OpenGL*. Aceasta determină diametrul punctelor în unități de coordonate de ecran. La fel ca `GL.LineWidth(float)`, această comandă se aplică global pentru toate punctele desenate în toată aplicația *OpenGL* și rămâne valabilă până când este modificată. Acest lucru nu operează în interiorul unei secțiuni `GL.Begin()`.
## 4. Răspundeți la următoarele întrebări (utilizați ca referință eventual și tutorii OpenGL Nate Robbins):
### • Care este efectul utilizării directivei *LineLoop atunci* când desenate segmente de dreaptă multiple în *OpenGL*?
### • Care este efectul utilizării directivei *LineStrip atunci* când desenate segmente de dreaptă multiple în *OpenGL*?
### • Care este efectul utilizării directivei *TriangleFan atunci* când desenate segmente de dreaptă multiple în *OpenGL*?
### • Care este efectul utilizării directivei *TriangleStrip* atunci când desenate segmente de dreaptă multiple în *OpenGL*?
## 5. Creați un proiect elementar. Urmăriți exemplul furnizat cu titlu de demonstrație - OpenGL_conn_ImmediateMode. Atenție la setarea viewport-ului.
## 6. Urmăriți aplicația „shapes.exe” din tutorii OpenGL Nate Robbins. De ce este importantă utilizarea de culori diferite (în gradient sau culori selectate per suprafață) în desenarea obiectelor 3D? Care este avantajul?
## 7.Ce reprezintă un gradient de culoare? Cum se obține acesta în OpenGL?
## 8. Creați o aplicație care la apăsarea unui set de taste va modifica culoarea unui triunghi (coordonatele acestuia vor fi încărcate dintr-un fișier text) între valorile minime și maxime, pentru fiecare canal de culoare. Ce efect va apare la utilizarea canalului de transparență? Aplicația va permite modificarea unghiului camerei cu ajutorul mouse-ului. Folosiți documentația disponibila la https://opentk.net/api/OpenTK.Input.Mouse.html
## 9. Modificați aplicația pentru a manipula valorile RGB pentru fiecare vertex ce definește un triunghi. Afișați valorile RGB în consolă.
## 10. Ce efect are utilizarea unei culori diferite pentru fiecare vertex atunci când desenați o linie sau un triunghi în modul strip?
