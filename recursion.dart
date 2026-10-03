import 'dart:io';

int SinCola(int N, int i){
  if (i > N){
    return 0; //No se suma nada extra entonces es 0
  } else {
    //La diferencia es que aqui la suma se hace despues de la llamada, a diferencia del otro
    return i + SinCola(N, i + 1);
  }
}

int Cola (int N, int i, int acumulado){ //parametros que se reciben y establecen
  if (i > N){ //si i es mayor que el numero de iteraciones en N se detiene y se devuelve el acumulado
    return acumulado;
  } else {
    acumulado = acumulado + i; //se suma el acumulado total con el numero donde va la iteracion
    i = i + 1; //se le suma en uno al numero de la iteracion de i
    return Cola(N, i, acumulado); //se devuelven los valores
  }
}

void main(){
  stdout.write('Ingresa la cantidad de iteraciones a realizar: '); //Bloque de comprobaciones para valores nulos
  String? iteracion = stdin.readLineSync();
  if (iteracion == null){
    print('Por favor ingrese un valor valido (numero entero): ');
  } else {
    int? N = int.tryParse(iteracion);
    if (N == null){
      print('Ingrese un valor valido por favor (numero entero): ');
    } else {
      try {
      Stopwatch cronometro = Stopwatch(); //Se crea el cronometro, inicia detenido en 0
      cronometro.start(); //Se inicia el contador del cronometro

      int r = Cola(N, 1, 0); //se guarda los valores que arrojo la version con cola en una variable

      cronometro.stop(); //Se detiene el contador
      print('Cola tardo: ${cronometro.elapsedMicroseconds} microsegundos'); //Se imprime el tiempo que tardo en hacer la operacion en milisegundos

      print('El resultado de la suma es: $r!'); //Se imprime el resultado de la suma
      } on StackOverflowError {
        print('Hubo stack overflow, no se pudo completar con este numero de iteraciones');
      }
      try {
        Stopwatch cronometro1 = Stopwatch();
        cronometro1.start();

        int r2 = SinCola(N, 1); //se guarda los valores que arrojo la version SinCola en una variable
        cronometro1.stop();
        print('Sin Cola tardo: ${cronometro1.elapsedMicroseconds} microsegundos');

        print('El resultado de la suma sin cola es: $r2!'); //Se imprime el resultado de la suma
      } on StackOverflowError{
        print('Hubo stack overflow, no se puede completar con este numero de iteraciones');
      }
      return;
    }
  }
}
