$(document).ready(function() {
    setInterval(function() {
        $.ajax({
            type: "get", url: "js/tempsrestant.php", cache: false, data: "heure=0&minute=0&seconde=0&mois=6&jour=20&annee=2011",
            success: function(html) {
                $(".hasCountdown").html(html);   
            }
        });
    }, 1000);
});