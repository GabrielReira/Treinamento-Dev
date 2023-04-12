function diaDaSemana(ano, mes, dia) {
  var data = (new Date(ano, mes-1, dia)).getDay();
  var dias = ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sab'];
  return dias[data];
}
