var lista = [1, 2, "Três"];
var elementos = document.querySelector('ul');

function removerElemento(index) {
  lista.splice(index, 1);
  atualizarLista();
}

function inserirElemento(event) {
  if (event.keyCode === 13) {  // Ao apertar 'Enter'
    lista.push(event.target.value);
    event.target.value = '';
    atualizarLista();
  }
}

function atualizarLista() {
  elementos.innerHTML = lista.map((item, index) => {
    return `<li onclick="removerElemento(${index})">${item}</li>`;
  }).join('');
}

atualizarLista();
