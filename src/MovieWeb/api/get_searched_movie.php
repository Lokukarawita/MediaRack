<?php
	$posted = json_decode(file_get_contents("php://input"));
	header('Content-Type: application/json');
	include($_SERVER['DOCUMENT_ROOT']."/testsite/api/tmdb-api.php");
	
	$movieid = $posted->movieid;
	
	$apikey = "500569e716c3a599be3b0b3f851092ad";
	$tmdb = new TMDB($apikey, 'en', true);
	
	
	$movie = $tmdb->getMovie($movieid);
	
	//Respose
	$resp_movie = array();
		
		$resp_movie['ID'] = $movie->getID();
		$resp_movie['Name'] = $movie->getTitle();
		$resp_movie['Plot'] = $movie->getOverview();
		$resp_movie['Votes'] = $movie->getVoteAverage();
		$resp_movie['RDate'] = $movie->getreleaseDate();
		$resp_movie['Runtime'] = $movie->getRunTime();
		$resp_movie['Genre'] = $movie->getGenres();
		$resp_movie['FCover'] = $tmdb->getImageURL('original'). $movie->getPoster();
		$resp_movie['BCover'] = $tmdb->getImageURL('original'). $movie->getBackdrop();
		
		$resp_movie['Trailer'] = $movie->getTrailer();
		//$testval = "Test";
		
		 // if(isset($moviename)){
				// $data = array(
					// "movieid"     => $moviename,
				// );
				// echo json_encode($data);
		 // }
	
	//Send Response
	echo json_encode($resp_movie);

?>