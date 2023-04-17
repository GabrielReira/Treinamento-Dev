function menor(numeros) {
  var menor = numeros[0];
  for(var i=0; i<numeros.length; i++) {
    if(numeros[i] < menor) {
      menor = numeros[i];
    }
  }
  return menor;
}
