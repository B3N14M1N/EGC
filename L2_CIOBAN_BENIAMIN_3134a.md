# Rezolvare Probleme
L2 Cioban Beniamin 3134a

## 1.
  Modificând valoarea constantei "MatrixMode.Projection"
## 2.

## 3.
### 1. Ce este un viewport?
  Un viewport este o regiune specifică a unei ferestre de desen în care se va afișa conținutul grafic.
### 2. Ce reprezintă conceptul de frames per seconds din punctul de vedere al bibliotecii OpenGL?
  În contextul bibliotecii OpenGL, conceptul de "frames per second" (FPS) se referă la măsurarea ratei de afișare a cadrelor randate pe ecran.
### 3. Când este rulată metoda OnUpdateFrame()?
  Metoda OnUpdateFrame() este apelată în fiecare cadru al ciclului aplicației după OnLoad() și înainte de OnRenderFrame().
### 4. Ce este modul imediat de randare?
  Modul imediat de randare este un concept în grafică computerizată în care fiecare instrucțiune de desenare este executată imediat, fără a stoca obiectele grafice în memorie.
### 5. Care este ultima versiune de OpenGL care acceptă modul imediat?
  Modul imediat de randare este suportat până la versiunea 3.5 OpenGL.
### 6. Când este rulată metoda OnRenderFrame()?
  Metoda OnRenderFrame() este rulată după ce metoda OnUpdateFrame() a fost apelată.
### 7. De ce este nevoie ca metoda OnResize() să fie executată cel puțin o dată?
  Metoda OnResize() trebuie să fie executată cel puțin o dată pentru a seta sau ajusta corect parametrii legați de dimensiunea ferestrei și/sau a zonei de desenare.
### 8. Ce reprezintă parametrii metodei CreatePerspectiveFieldOfView() și care este domeniul de valori pentru aceștia?
  Parametrii metodei CreatePerspectiveFieldOfView() sunt:
      public static void CreatePerspectiveFieldOfView (float fovy, float aspect, float zNear, float zFar);
  - fovy (field of view): 
  - aspect: 
  - zNear: 
  - zFar:
