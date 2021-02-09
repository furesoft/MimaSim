# Assemblycode
Der Assemblycode ist gewissermaßen genauso aufgebaut wie der Maschinencode, der Unterschied ist die Lesbarkeit.

Aufbau eines Befehls:
```
Mnemnonic Argument1, Argument2
```

Ein Beispiel lädt die Konstante 42 in das Register Accumulator:

```
mov 42, Accumulator
```

Möchte man auf einer Adresse im Speicher verweisen:

```mov A2, &42```

Der Wert A2 wird an die Speicherstelle 42 geschrieben