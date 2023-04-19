var listFilter = '';
var listElement = $('#pokeList');
var inputElement = $('#pokeFilter');
var pokeballElement = $('#pokeballBack');


inputElement.on('keyup', function() {
	listFilter = this.value;
	setList();
})

$(window).on('scroll', function() {
	var rotation = `translateY(-50%) rotateZ(${window.scrollY / 15} deg)`;
  pokeballElement.css('transform', rotation);
})

function setList(){
	PokeService.listAll(function(pkmList) {
    var list = filterList(pkmList);
    var template = ListService.createList(list);
    listElement.html(template);
  })
}

function filterList(pkmList) {
	return pkmList.filter(function(pkm) {
    return pkm.name.indexOf(listFilter.toLowerCase()) !== -1;
  });
}

setList();

// Plugin 'jump'
$('#pokeList').on('click', 'li', function() {
  $(this).find('img').jump();
})
