INSTRUCCIONES PRUEBA TÉCNICA UNITY DEVELOPER PARA LABCAVE


Cosas añadidas:
 - Carpetas MyScripts (en Karting\Scripts), MyPrefabs (Karting\Prefabs), MyMaterials (Karting\Materials), y Audio (Karting\Audio). 
Soy consciente de que estas dos últimas no eran necesarias, pero costaban muy poco, y en mi opinión aportaban mucho.
 - Nueva Scene "MainMenu" en Karting\Scenes. Además, he renombrado la escena principal a "Race".

----------------------------------------

A continuación, voy a ir hablando de qué he hecho en cada paso de la prueba. Copio y pego los requisitos:

 - "Incluyas una escena inicial llamada MainMenu con dos botones de Play y Exit, el
primero iniciará y cargará la segunda escena de gameplay, y el segundo terminará el
juego."

He añadido la escena "MainMenu", donde he añadido un Canvas con algunos elementos gráficos (titulo, background...) y dos los botones. 
Además, en la escena hay un objeto llamado "SceneLoader" con un script "SceneLoader.cs" adjuntado. 
Aquí hay varios métodos públicos con distintas funcionalidades para cada posible botón (no sólo en esta escena, sino también en la escena de juego).
Los botones tienen todos el mismo comportamiento: al pulsar se ejecuta un sonido para dar feedback de que ha sido presionado y se inicia una rutina en la que después de 0.5 segundos carga la escena deseada (en este caso, carga la escena de juego, o quita la applicación)

----------------------------------------

 - "Almacenes localmente el mejor tiempo de vuelta que el jugador haya hecho llamado
Best Time."
 - "Muestra en la escena de MainMenu el tiempo de mejor vuelta de jugador registrado
hasta el momento."

Aquí empecé usando PlayerPrefs para guardar la ingormación, pero me di cuenta de que la plantilla de Unity ya guarda las puntuaciones. 
Hace esto desde el script "TrackRecord.cs", el cual yo no he modificado en absoluto. 
He visto que podía usar la función estática TrackRecord.Load() para cargar la información correspondiente a una vuelta en la pista "ArtTest", que es como se refiere a la pista de la escena de juego.
Para hacer esto, añadí un objeto nuevo a MainMenu llamado BestScoreDisplay, con el script BestScoreDisplay.cs, que se encarga de escribir en el MainMenu el mejor tiempo (así como el número total de monedas, vuelvo a eso más adelante)

----------------------------------------

 - "Al terminar las tres vueltas aparezca un menú de Game Over con dos botones, Play Again y
Back to Main Menu, el primero volverá a iniciar la partida de nuevo y el segundo cargará la
escena de MainMenu"

Para esto cree un objeto Canvas llamado GameOverMenuCanvas en la escena "Race", y añadí, de manera similar a en el Main Menu, texto y botones (todos los botones provienen del mismo prefab). 
Este objeto está desactivado por defecto, y se activa sólo cuando se acaba la carrera. 
Para hacer esto, modifiqué el script TrackManager.cs, y en el método StopRace() añadí unas líneas para activar el GameOverMenuCanvas y transmitir la información de la carrera (info del tiempo de tu mejor vuelta, y el record de mejor vuelta).
Añadí una referencia pública al GameOverMenuCanvas para decirle desde el inspector al objeto TrackManager qué canvas queremos que active al acabar la carrera.
Dicho objeto tiene que ser de tipo GameOverMenuCanvas, que es el script adjunto a este objeto.

----------------------------------------

 - "Añade items Coins que aparezcan durante el recorrido que vaya recogiendo el jugador.
▪ Como extra, añade además un contador de monedas que tenga funcionalidad similar
al del Best Time, durante el gameplay el jugador deberá ver el total de monedas de
esa carrera. En la escena de Main Menu deberá mostrar el total de monedas
acumuladas entre varias carreras."

Usando simples cilindros de Unity, he creado objetos Coins, y puesto varios por la pista. 
También he añadido un material de moneda y un sonido para cuando se recoge la moneda.
He añadido dos scripts para esto: Coin.cs y CoinManager.cs.
Coin.cs está como componente de todos los objetos Coins y su función principal es dar una animación de rotación constante a las monedas, y que cuando una moneda sea recogida por el jugador (a través de OnTriggerEnter), ejecute un sonido, se destruya, y añada +1 a la puntuación de las monedas.
La puntuación de las monedas la controla CoinManager.cs, y con la función pública PickedUpCoin(), se suma uno al total de monedas recogidas en la pista. 
Cada vez que esta función se llama se actualiza el texto CoinText de CoinCounterCanvas. 
Al final de la carrera, TrackManager.cs llama la función pública SetCoinsNumber() del objeto CoinManager, para lo cual he añadido una referencia pública en TrackManager.
Esta función añade en PlayerPrefs el total de monedas conseguidas en esta carrera. 
De esta manera, BetScoreDisplay puede conseguir la información del número total de monedas en todas las carreras de PlayerPrefs.



- "Haz que toda la interfaz sea “responsive” y se adapte a los distintos tamaños de
dispositivos (landscape)."

Para esto sencillamente me he asegurado de que todos los objetos Canvas estan con Scale Mode = Scale With Screen Size, y con una resolutición de referencia apaisada.
Probando distintas resoluciones he comprobado que siempre se veía bien.



 - "Cada vez que se termine una vuelta y se haya superado la puntuación del Best Time que
aparezca un pequeño mensaje de texto que desaparezca al poco tiempo durante la partida
informando de un New Record"

Para esto he ido a la línea de código en TrackManager.cs en donde chequea si la nueva vuelta es record, en la función RacerHitCorrectCheckpoint().
Ahí, si el tiempo de vuelta es menor que el mejor histórico, se hacen dos cosas: se establece el nuevo tiempo con record, y se activa un nuevo Canvas que he creado.
Este canvas, al igual que GameOverMenuCanvas, se inicia desactivado, y solo se activa desde aquí. He creado un script NewRecordCanvas.cs en donde hay una función OnEnable().
Esto hace que cuando se llame la función desde el TrackManager, se active una pequeña animación en el texto "New Record" en la que su tamaño oscila.
Se que esto no era necesario, pero me parecía sencillo añadirlo y quedaba bien. 
Después de un tiempo, que se puede controlar desde el inspector, este canvas se vuelve a desactivar.



 - "Genera una build y haz que el juego funcione correctamente en un dispositivo Android.
▪ No hace falta que el juego sea “jugable” en móvil pero puedes mapear los controles como
quieras, por ejemplo, pulsación en parte izquierda de la pantalla, giro a la izquierda y análogo
en la derecha, para el derrape hazlo como creas que puede ser más cómodo al usuario.
▪ Sube al repositorio un .apk (o un .aab) con soporte a arquitectura ARM64."

En la última versión del juego, he adaptado todo para que sea jugable en Android, acorde a la descripción. 
Para eso, he añadido un nuevo script al objeto Kart, llamado TouchInput.cs. 
Este script está siguiendo la plantilla de los scripts Gamepad- y KeyboardInput.cs.
Sin embargo, permite que el juego responda a controles táctiles. 
El kart acelera por defecto, y para girar hay que tocar cada lado de la pantalla.

