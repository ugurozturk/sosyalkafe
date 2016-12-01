
//JQuery nin çalıştığına emin ol
if (typeof jQuery === "undefined") {
  throw new Error("jQuery yi ayarla");
}

var resimler = new Array();


$(document).ready(function () {
    g();
    setInterval(function(){g()}, 60000); //dakikada bir
});

function g() {
    $.ajax({
            type: "POST",
            dataType: "json",
            url: "/uygulama/resimcek",
            data: {
                firmaadi : firmaadi
            },
            success: function (data) {
                console.log(data.resimlerList);
                $(data.resimlerList).each(function (index) {
                    if (resimler.indexOf(data.resimlerList[index].resimid) == -1) {
                        resimler.push(data.resimlerList[index].resimid);
                        resimYenile(data.resimlerList[index].resimAdres);
                    }
                });
            }
        });
}

function resimYenile(resim) {

    if ($(".instagramWidget").children().length < 6) {
    $(".instagramWidget").prepend("<div class=\"item\"><img src=\"" + resim + "\" width=\"100% \"></div>");
    } else {
        $(".instagramWidget").children().last().remove();
        $(".instagramWidget").prepend("<div class=\"item\"><img src=\"" + resim + "\" width=\"100% \"></div>");
    }
    

}