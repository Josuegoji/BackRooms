# BackRooms

Juego de terror y exploración en realidad virtual ambientado en las Backrooms. Proyecto de clase en equipo, desarrollado en Unity para explorar las herramientas nativas de VR del motor.

> 🎥 **Video de gameplay:** https://youtu.be/nd6wTwk1NYs

## De qué trata

Exploras las salas de las Backrooms buscando la salida. El juego tiene tres zonas, y hay que completar cada una para pasar a la siguiente.

**Zona 1: Inicio**
La zona inicial en la que se te da la herramienta que usaras durante todo el juego, una cámara. Hay pistas ocultas que solo se ven a través de la cámara, y para usarla tienes que sostenerla frente a tus ojos. La cámara también sirve de defensa: si le tomas una foto al monstruo, este se teletransporta lejos de ti. Además, hay objetos que se pueden tomar y colocar en sockets, que sirve para desbloquear la siguiente zona. El monstruo solo te sigue aquí. 

**Zona 2: Los pasillos**
Una serie de pasillos donde, si no vuelves por cierto punto, regresas al inicio de la sección sin darte cuenta, inspirado en*The Exit 8*.

**Zona 3: La sala inundada**
Una sala que se va llenando de agua mientras buscas un código oculto con la cámara. Al ingresarlo en un panel, se desbloquea el final del juego.

## Mi participación

Proyecto en equipo. Mi parte fue la programación de estas mecánicas y del flujo general del juego:


- **Jugador:** Modificación para que no atrviese paredes.
- **Enemigo:** [comportamiento: persecución, patrulla, estados, etc.] y su reacción al ser fotografiado (teletransporte).
- **Cámara VR:** detección de cuándo está frente a los ojos del jugador, visión de pistas ocultas a través de ella y detección del enemigo para la foto, ademas de la "munición" de esta, los rollos de camara.
- **Sistema de objetos y sockets:** agarrar objetos y colocarlos en sockets.
- **Pasillo infinito:** lógica de reinicio sin que el jugador lo note.
- **Sala de agua:** subida progresiva del nivel del agua y panel de ingreso del código.
- **Shaders (Shader Graph):** efecto de glitch al fotografiar al enemigo y efecto de construcción en las puertas secretas.
- **Flujo del juego:** progresión entre zonas.

## Tecnologías

- Unity 6000.3.9f1
- C#
- XR Interaction Toolkit 3.3.1
- XR Plugin Management
- Shader Graph

## Scripts destacados

- [`Enemigo`](Assets/Scripts/Enemigo.cs)
- [`Cámara`](Assets/Scripts/Camara.cs)
- [`Jugador`](Assets/Scripts/jugador.cs)
- [`Pasillo infinito`](Assets/Scripts/Teletransporte.cs)
- [`Sala de agua`](Assets/Scripts/Agua.cs)
- [`Shader de contrucción`](Assets/Scripts/Construir.shadergraph)
- [`Shader de glitch`](Assets/Scripts/glich.shadergraph)

## Cómo abrirlo

1. Abre el proyecto con Unity 6000.3.9f1.
2. Abre la escena "SampleScene".
3. Requiere un visor de realidad virtual compatible con XR Interaction Toolkit.

o puedes probar el ejecutable. 

## Estado

Proyecto de clase, completo y jugable. No está totalmente pulido.

