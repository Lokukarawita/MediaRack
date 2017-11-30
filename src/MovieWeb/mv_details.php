<html>
<head>
<!--jQuery CDN-->
<script src="http://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
</head>
<body>
<?php

 include("/api/tmdb-api.php");

            $apikey = "500569e716c3a599be3b0b3f851092ad";
            $tmdb = new TMDB($apikey, 'en', true);
			
			echo '<li><a id="movieInfo"><h3>Full Movie Info</h3></a>';
			//$val = parse_url($url);
            $movie = $tmdb->getMovie($_GET['id']);
            echo 'Now the <b>$movie</b> var got all the data, check the <a href="http://code.octal.es/php/tmdb-api/class-Movie.html">documentation</a> for the complete list of methods.<br><br>';

            echo '<b>'. $movie->getTitle() .'</b><ul>';
            echo '  <li>ID:'. $movie->getID() .'</li>';
            echo '  <li>Tagline:'. $movie->getTagline() .'</li>';
            //echo '  <li>Trailer: <a href="https://www.youtube.com/watch?v='. $movie->getTrailer() .'">link</a></li>';
            echo '</ul>...';
            echo '<img src="'. $tmdb->getImageURL('w185') . $movie->getPoster() .'"/></li>';

       
?>
</body>
</html>