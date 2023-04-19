(function ($) {
  $.fn.jump = function() {
    $(this)
      .stop()
      .animate({
        'marginTop': '-2em'
      }, 150)
      .animate({
        'marginTop': '0em'
      }, 150)
      .animate({
        'marginTop': '-2em'
      }, 150)
      .animate({
        'marginTop': '0em'
      }, 150)

    return this;
  }
})(jQuery)
