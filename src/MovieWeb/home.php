<?php
ob_start();
session_start();
?>
<html lang="en">

    <?php
//variables for movie
$movi_name;
$movi_act;
$movi_avgnote;
$movi_rdte;
$movi_runtime;
$movi_genre;
$movi_dvdno;

$movies = array();

mv_data_load();

//DB connection to mv_db database function
function mv_data_load(){

    //Connection to the database
    include($_SERVER['DOCUMENT_ROOT']."/testsite/api/dbconnection/connection.php");



    // $idx = 1;
    global $movies;
    global $cache_id;
    //    if (mysqli_num_rows($result) > 0) {
    //        // output data of each row
    //        while($row = mysqli_fetch_assoc($result)) {
    //            $movies[$idx][0] = $row["Id"];
    //            $movies[$idx][1] = $row["Watched"];
    //            $movies[$idx][2] = $row["WatchedOn"];
    //            $movies[$idx][3] = $row["Favorite"];
    //            $movies[$idx][4] = $row["Grade"];
    //            $movies[$idx][5] = $row["Comment"];
    //            $movies[$idx][6] = $row["Bookmark"];
    //            $movies[$idx][7] = $row["ImageCacheID"];
    //            $movies[$idx][8] = $row["Timestamp"];
    //            $idx++;
    //
    //        }
    //        echo $row["mv_plot"];
    //    }

    //get last selected movie name from the DB
    $get_cache_id = "SELECT * FROM mv_cache";
    $result_cache = mysqli_query($conn, $get_cache_id);

    while($row_cache = mysqli_fetch_assoc($result_cache)) {
        $cache_id = $row_cache["mv_name_id"];
    }
    $conn->close();
}

    ?>

    <head>

        <meta charset="utf-8">
        <meta http-equiv="X-UA-Compatible" content="IE=edge">
        <meta name="viewport" content="width=device-width, initial-scale=1">
        <meta name="description" content="">
        <meta name="author" content="">

        <title>Movie DB</title>

        <!-- Bootstrap Core CSS -->
        <link href="css/bootstrap.min.css" rel="stylesheet">

        <!-- Custom CSS -->
        <link href="css/portfolio-item.css" rel="stylesheet">

        <!-- Main CSS -->
        <link href="css/style.css" rel="stylesheet">

        <!--Fontaweosme CDN-->
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">

        <!--Fonts-->
        <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro" rel="stylesheet">

        <!--Scrollbar-->
        <link href="css/scrollbar.css" rel="stylesheet">

        <!--jQuery JS-->
        <script src="js/jquery-2.2.1.min.js"></script>

        <!--Bootstrap JS-->
        <script src="js/bootstrap.min.js"></script>

        <!--Circliful-->
        <link rel="stylesheet" href="css/jquery.circliful.css" />

        <script type="text/javascript">
            $('document').ready(function () {
                updatestatus();
                scrollalert();
            });
            function updatestatus() {
                //Show number of loaded items
                var totalItems = $('.searchable tr').length;
                $('#status').text('Loaded ' + totalItems + ' Items');
            }
            function scrollalert() {
                var scrolltop = $('#movie_list_table table').attr('scrollTop');
                var scrollheight = $('#movie_list_table table').attr('scrollHeight');
                var windowheight = $('#movie_list_table table').attr('clientHeight');
                var scrolloffset = 20;
                if (scrolltop >= (scrollheight - (windowheight + scrolloffset))) {
                    //fetch new items
                    $('#status').text('Loading more items...');
                    $.get('new-items.html', '', function (newitems) {
                        $('#content').append(newitems);
                        updatestatus();
                    });
                }
                setTimeout('scrollalert();', 1500);
            }
        </script>

    </head>


    <?php
//Echo session variables that were set on previous page

if(!isset($_SESSION["email"]) && !isset($_SESSION["password"])){
    header("location: index.php?error=1");
}
    ?>

    <body id="animate-area">
        <!--id="animate-area"-->

        <!--TMDB Api apikey-->
        <?php
include($_SERVER['DOCUMENT_ROOT']."/testsite/api/tmdb-api.php");

$apikey = "500569e716c3a599be3b0b3f851092ad";
$tmdb = new TMDB($apikey, 'en', true);	
        ?>

        <label style="display:none" id="cache_id"><?php echo $cache_id; ?></label>
        <label style="display:none" id="user_id"><?php echo  $_SESSION['userid']; ?></label>

        <!-- Navigation -->
        <nav class="navbar navbar-inverse navbar-fixed-top" role="navigation">
            <div class="container">

                <!-- Brand and toggle get grouped for better mobile display -->
                <div class="navbar-header">
                    <!--<a class="navbar-brand" href="#">Movie DB</a>-->
                    <div class="logo"></div>
                </div>

                <!-- Collect the nav links, forms, and other content for toggling -->
                <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">
                    <ul class="nav navbar-nav">

                        <!-- Logout button on the navigation bar -->
                        <li>
                            <a href="logout.php"><i class="fa fa-sign-out" aria-hidden="true"></i></a>
                        </li>
                    </ul>
                </div>
            </div>

        </nav>

        <!-- Page Content -->

        <div id="m_BCover" class="container" style="text-align:center">
            <img id="mask_loading" src="https://zippy.gfycat.com/ImpoliteLivelyGenet.gif" />
            <div class="container BCover_mask">

                <!-- Side Panel -->
                <!-- Page Content -->
                <div class="movie_container_nav">
                    <button class="movie_list_btn" onclick="openNav()"><i style="font-size:30px" class="fa fa-bars" aria-hidden="true"></i></button>
                    <button class="movie_list_btn" onclick="openMovieNav()" style="float:right"><i style="font-size:30px;" class="fa fa-film" aria-hidden="true"></i></button>
                </div>

                <!--Movie List Navigation Panel-->
                <div id="mySidenav" class="sidenav">
                    <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a>
                    <div style="background-color:white; padding: 5px 45px 0px 45px;" class="col-md-12">
                        <!--Movie Search Box-->
                        <div class="input-group"> <span class="input-group-addon">Search</span>
                            <input id="filter" type="text" class="form-control" placeholder="Type here..." />
                        </div>

                        <!--Movie Details List-->
                        <div id="movie_list_table">
                            <table class="table table-hover table-striped" id="moviesTbl" style="cursor:pointer">
                                <thead>
                                    <tr>
                                        <th>ID</th>
                                        <th>Movie Name</th>
                                        <th>DVD</th>
                                        <th>Rating</th>
                                    </tr>
                                </thead>
                                <tbody class="searchable">
                                    <?php 
$cnt = count($movies);

for($x = 0; $x < $cnt; $x++) {
    echo "<tr>";
    echo "<td>".$movies[$x][0]."</td>";
    echo "<td>".$movies[$x][1]."</td>";
    echo "<td>".$movies[$x][7]."</td>";
    echo "<td>".$movies[$x][4]."</td>";
    echo "</tr>";
}	
                                    ?>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>

                <!--Movie List Navigation Panel-->
                <div id="Movie_Search" class="sidenav">
                    <a href="javascript:void(0)" class="closebtn" onclick="closeMovieNav()">&times;</a>
                    <div style="background-color:white;" class="col-md-12">

                        <div class="col-md-12">
                            <!--Movie Content Here-->
                            <div class="row">
                                <div class="input-group">
                                    <span class="input-group-btn">
                                        <button id="Movie_Search_Btn" class="btn btn-default" type="button" style="color: #fff; border-color: #009688; background-color: #009688;">Movie Search</button>
                                    </span>
                                    <input id="Movie_Search_Txt" type="text" class="form-control" value="Underworld" placeholder="Search for..." />
                                </div>
                            </div>
                        </div>
                        <div id="movie_search_table" class="col-md-12" style="margin-top: 20px;">
                            <table style="font-family: 'Source Sans Pro', sans-serif;" class="table table-striped">
                                <tbody id="Movie_Serach_Results">
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>


                <!-- Movie content information -->
                <div class="row movie_content">
                    <div class="col-md-12">

                        <!--Get Movie Cover From TMDB-->
                        <div class="col-md-4 movie_cover">
                            <img id="m_FCover" style="width: 100%;" class="img-responsive" src="" alt="" />
                        </div>

                        <!--Movie Description-->
                        <div class="col-md-8 movie_det">
                            <!--Movie Title-->
                            <h1 class="page-header"><span id="m_Title"></span></h1>

                            <!--test-->
                            <div class="row">
                                <div class="col-lg-2">

                                </div>
                            </div>
                            <!--end test-->

                            <table>
                                <tr>
                                    <th width="100px">
                                        <!--Movie Vote Circle-->
                                        <div id="test-circle"></div>
                                    </th>
                                    <th>
                                        <!--Movie Trailer Link-->
                                        <a id="movie_trailer" target="_blank">
                                            <h3 style="margin-top: 15px;"><i style="font-size: 18px;" class="fa fa-play" aria-hidden="true"></i>
                                                <span
                                                      style="font-size: 15px; margin-left: 8px;">Play Trailer</span>
                                            </h3>
                                        </a>
                                    </th>
                                    <th>
                                        <!--Add Movie-->
                                        <button id="add_btn" type="button" class="btn add_db" data-toggle="modal" data-target="#movie_alertModel"><i class="fa fa-cloud-upload" aria-hidden="true"></i><span style="font-size: 15px; margin-left: 8px;">Save</span></button>
                                    </th>
                                </tr>
                            </table>

                            <!--Movie Plot-->
                            <h3>Overview</h3>
                            <p id="m_Plot"><span></span></p>

                            <!--Movie Actors-->
                            <h3>Featured Crew</h3>
                            <p id="m_Actors"></p>

                            <!--Movie Genre-->
                            <h3>Genere</h3>
                            <p id="m_Genre"></p>

                            <!--Movie Run Time-->
                            <h3><span id="m_Runtime"></span></h3>

                            <!--Movie Relese Date-->
                            <h3><span id="m_RDate"></span></h3>

                            <!--Movie Genre-->
                            <p id="m_Genre"></p>

                            <!--Average Votes-->
                            <p style="display:none" id="m_AvgVotes"></p>

                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- jQuery -->
        <script src="js/jquery.js"></script>

        <!-- Bootstrap Core JavaScript -->
        <script src="js/bootstrap.min.js"></script>

        <div id="result"></div>

        <!-- Alert Modal -->
        <div class="modal fade" id="movie_alertModel" role="dialog">
            <div class="modal-dialog modal-sm">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title"><i class="fa fa-bell" aria-hidden="true"></i><span style="margin-left: 10px;">Alert</span></h4>
                    </div>
                    <div class="modal-body">
                        <p id="movie_model_text"></p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>

            </div>
        </div>

        <script src="js/jquery.circliful.js"></script>
        <script>
            //Search box in movie table
            $(document).ready(function () {

                var movie_id;
                var votes;

                (function ($) {
                    $('#filter').keyup(function () {
                        var rex = new RegExp($(this).val(), 'i');
                        $('.searchable tr').hide();
                        $('.searchable tr').filter(function () {
                            return rex.test($(this).text());
                        }).show();
                    })
                }(jQuery));

                //load last movie when open the page
                value = $("#cache_id").text();
                user_id = $("#user_id").text();

                var objX = {
                    "mvid": value,
                    "userid": user_id
                };

                $.ajax({
                    type: 'POST',
                    url: 'api/get_movie.php',
                    data: JSON.stringify(objX),
                    contentType: "application/json",
                    success: function (r) {

                        console.log(r);
                        if (r.Status) {
                            //
                            $('#m_Title').html(r.Movie.Name);
                            $('#m_Plot').html(r.Movie.Plot);
                            $('#m_RDate').html(r.Movie.RDate);
                            $('#m_Runtime').html(r.Movie.Runtime);
                            //$('#m_Actors').html(r.Movie.Actors);
                            //$('#m_Genre').html(r.Movie.Genre);
                            $('#m_AvgVotes').html(r.Movie.AvgVotes);
                            $("#m_FCover").attr("src", r.Movie.FCover);
                            $("#m_BCover").css('background-image', 'url(' + r.Movie.BCover + ' )');
                            //$("#movie_trailer").attr('href', "https://www.youtube.com/watch?v=" + r.Movie.Trailer);
                            //
                            votes_graph(r.Movie.AvgVotes);
                            //
                        }
                    },
                    fail: function (x, y, z) {
                        console.log(x);
                    }
                });

                //Load selected movie when open the page
                $("body #moviesTbl tr").click(function () {
                    var tableData = $(this).children("td").map(function () {
                        return $(this).text();
                    });
                    closeNav();

                    /*Hide add movie data button*/
                    $(".add_db").css("display", "none");


                    var objX = {
                        "mvid": tableData[0]
                    };

                    $.ajax({
                        type: 'POST',
                        url: 'api/get_movie.php',
                        data: JSON.stringify(objX),
                        contentType: "application/json",
                        success: function (r) {
                            if (r.Status) {
                                $("svg").remove();
                                $(".movie_content").fadeOut(500, function () {

                                    $('#m_Title').html(r.Movie.Name);
                                    $('#m_Plot').html(r.Movie.Plot);
                                    $('#m_RDate').html(r.Movie.RDate);
                                    $('#m_Runtime').html(r.Movie.Runtime);
                                    $('#m_Actors').html(r.Movie.Actors);
                                    $('#m_Genre').html(r.Movie.Genre);
                                    $('#m_AvgVotes').html(r.Movie.AvgVotes);
                                    $("#m_FCover").attr("src", r.Movie.FCover);
                                    $("#m_BCover").css('background-image', 'url(' + r.Movie.BCover + ' )');
                                    $("#movie_trailer").attr('href', "https://www.youtube.com/watch?v=" + r.Movie.Trailer);

                                    votes_graph(r.Movie.AvgVotes);


                                    //Change Movie Cover
                                }).fadeIn(500);
                                return false;
                            }
                        },
                        fail: function (x, y, z) {
                            console.log(x);
                        }
                    });

                });

                //Search Movie from TMDB
                $("#Movie_Search_Btn").click(function () {
                    movie_search();
                });

                $(document).keypress(function (e) {
                    if (e.which == 13) {
                        movie_search();
                    }
                });

                /*Movie serach list*/
                function movie_search() {
                    var movienme = $("#Movie_Search_Txt").val();

                    $.ajax({
                        type: "POST",
                        url: "api/get_movie_det.php",
                        data: "moviename=" + movienme,
                        success: function (r) {

                            /*Load content to the movie list table*/
                            $("#Movie_Serach_Results").html(r);

                            /*Show alert when movie name is incorrect*/
                            if (r == "") {
                                $("#movie_model_text").text("Please enter correct movie name");
                                $("#movie_alertModel").modal("show");
                            }

                            /*Click movie from the search list*/
                            $("#Movie_Serach_Results>tr").click(function () {
                                var sts = $('.mvid', this).text();

                                //console.log(sts);
                                get_movie_searched_det(sts);
                                closeMovieNav();
                            });

                        }
                    });
                };

                /*Get searched movie details*/
                function get_movie_searched_det(movieid) {

                    var mv = { movieid: movieid };
                    $.ajax({
                        type: 'POST',
                        url: 'api/get_searched_movie.php',
                        data: JSON.stringify(mv),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (r) {

                            //Fade feature for the movie content when it's load to the container
                            $(".movie_content").fadeOut(500, function () {
                                /*Remove previous vote*/
                                $("svg").remove();
                                movie_id = r.ID;
                                $('#m_Actors').html("");

                                //Load movie content 
                                $('#m_Title').html(r.Name);
                                $('#m_Plot').html(r.Plot);
                                $('#m_RDate').html(r.RDate);
                                $('#m_Runtime').html(r.Runtime + " min");
                                $('#m_Plot').html(r.Plot);
                                $('#m_AvgVotes').html(r.Votes);
                                $("#m_FCover").attr("src", r.FCover);
                                $("#m_BCover").css('background-image', 'url(' + r.BCover + ' )');
                                $("#movie_trailer").attr('href', "https://www.youtube.com/watch?v=" + r.Trailer);
                                $('#m_Genre').html(genstr);

                                votes_graph(r.Votes);

                                //Get movie cast details
                                get_cast(movie_id);

                                var genstr = r.Genre.map(function (elem) {
                                    return elem.name;
                                }).join("| ");

                                /*Enable add movie data button*/
                                $(".add_db").css("display", "block");


                            }).fadeIn(500);
                            return false;

                        },
                        error: function (x, y, z) {
                            console.log(x);
                        }
                    });
                }

                /*Get cast details*/
                function get_cast(movi_id) {
                    var apikey = "500569e716c3a599be3b0b3f851092ad";

                    $.ajax({
                        type: "GET",
                        url: 'https://api.themoviedb.org/3/movie/' + movi_id + '/credits?api_key=' + apikey,

                        success: function (r) {

                            var ar = [];

                            var castr = r.cast.slice(0, 5).map(function (elem) {
                                return elem.character;
                            }).join(", ");


                            $('#m_Actors').html(castr);

                        },
                        error: function (x, y, z) {
                            console.log(x);
                        }
                    });
                }

                /*Save movie details to database*/
                $("#add_btn").click(function () {

                    var sv = {
                        "movieid": movie_id,
                        "moviename": $("#m_Title").text(),
                        "movieactors": $("#m_Actors").text(),
                        "moovievotes": $("#m_AvgVotes").text(),
                        "movierdate": $("#m_RDate").text(),
                        "movieruntime": $("#m_Runtime").text(),
                        "moviegenere": $("#m_Genre").text()
                    };

                    $.ajax({
                        type: "POST",
                        url: "api/save_movie.php",
                        data: JSON.stringify(sv),
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (r) {

                            $("#movie_model_text").text(r.record);

                        },
                        error: function (x, y, z) {
                            console.log(x);
                        }
                    });
                });

            });

            /*Deafult slide size*/
            var wdth = "550px";

            /*Set window width to slider width variable*/
            var site_width = $(window).width();

            if (site_width < 600) {
                wdth = site_width + "px";
            }

            //Close the slide panels when click on the container
            $(".movie_det").click(function () {
                document.getElementById("mySidenav").style.width = "0";
                document.getElementById("Movie_Search").style.width = "0";
            });

            //Open saved movies side panel
            function openNav() {
                document.getElementById("mySidenav").style.width = wdth;
                document.getElementById("Movie_Search").style.width = "0";
                var nav_height = $('#m_BCover').height();
                $(".sidenav").height(nav_height - 87);
                $("#movie_list_table , #movie_search_table").height(nav_height - 200);

            }

            //Close saved movies side panel
            function closeNav() {
                document.getElementById("mySidenav").style.width = "0";
            }

            //Open movie search side panel
            function openMovieNav() {
                document.getElementById("Movie_Search").style.width = wdth;
                document.getElementById("mySidenav").style.width = "0";
                var nav_height = $('#m_BCover').height();
                //Change sidepanel side according to the background div size
                $(".sidenav").height(nav_height - 87);
                $("#movie_list_table , #movie_search_table").height(nav_height - 200);
            }

            function closeMovieNav() {
                document.getElementById("Movie_Search").style.width = "0";
            }

            $(".BCover_mask").hide();
            $("#m_BCover").css('background-color', 'white');

            //AJAX complete event	
            $(document).ajaxComplete(function () {
                $(".BCover_mask").fadeIn(1000);
                $("#mask_loading").hide();
                $("#m_BCover").css("text-align", "inherit");
            });

            /*Movie vote circle*/
            function votes_graph(vte) {

                var prese = "50";
                var votes_tot = 0;

                votes_tot = (vte / 10) * 100;

                $(document).ready(function () {
                    $("#test-circle").circliful({
                        animation: 1,
                        animationStep: 5,
                        foregroundBorderWidth: 15,
                        backgroundBorderWidth: 15,
                        percent: votes_tot,
                        textSize: 28,
                        textStyle: 'font-size: 12px;',
                        textColor: '#666',
                        fontColor: '#333333',
                        multiPercentage: 1,
                        percentages: [10, 20, 30]
                    });
                });
            }

        </script>
    </body>
</html>