<?php
	/*Movie id*/
	$moviename=$_POST['moviename'];
	
	/*TMDB API path*/
	include("../api/tmdb-api.php");
	
	$apikey = "500569e716c3a599be3b0b3f851092ad";
	$tmdb = new TMDB($apikey, 'en', true);
		
		$movies = $tmdb->searchMovie($moviename);
            foreach($movies as $movie){
				
				$moviePoster = $movie->getPoster();
				
				if($moviePoster===NULL){
					$mvePoster = "/testsite/images/image_not_available.jpg";
				}
				else{
					$mvePoster = 'http://image.tmdb.org/t/p/w185/'.$movie->getPoster();
				}
				
				/*Response*/
                echo '<tr><td style="width: 80px;"><img style="border: 1px solid #b7b7b7;" src="'.$mvePoster.'" height="80" width="53"></td><td><span style="font-size: 16px;
    font-weight: 700;">'. $movie->getTitle().'</span></br><span>'.$movie->getreleaseDate().'</span></br><img src="http://orig12.deviantart.net/aea1/f/2011/292/a/8/hollow_star_png_by_vin8it-d4d6uza.png" width="15" height="15"><span style=" margin-left: 10px;">'.$movie->getvoteAverage().'</span><span class="mvid" style="display:none">'. $movie->getID() .'</span></td></tr>';
            }
	//echo $moviename;		
	
	//Send Response
	//echo json_encode($resp_movie);
	
?>