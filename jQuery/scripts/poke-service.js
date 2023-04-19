var PokeService = {
  url: '//dev.treinaweb.com.br/pokeapi/',
	list: [],
	listAll: function(callback){
		if(this.list.length) {
			callback(this.list);
		}
    else {
      var that = this;

      $.ajax(this.url + 'pokedex/1').then(function(response) {
        var pkmList = response.pokemon;

        pkmList = pkmList.map(function(pokemon) {
          var number = that.getNumberFromURL(pokemon.resource_uri);
          pokemon.number = number;
          return pokemon;
        })
        .filter(function(pokemnon) {
          return (pokemon.number < 1000);
        })
        .sort(function(a, b) {
          return (a.number > b.number ? 1 : -1);
        })
        .map(function(pokemnon) {
          pokemnon.number = ('000' + pokemnon.number).slice(-3);
          return pokemnon;
        })

        that.list = pkmList;
        callback(pkmList);
      })
    }
  },
  getNumberFromURL: function(url) {
    return parseInt(url.replace(/.*\/(\d+)\/$/, '$1'));
  }
}
