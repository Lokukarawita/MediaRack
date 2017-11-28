<?php
	$posted = json_decode(file_get_contents("php://input"));
	header('Content-Type: application/json');
	include($_SERVER['DOCUMENT_ROOT']."/testsite/api/tmdb-api.php");
	
	$apikey = "500569e716c3a599be3b0b3f851092ad";
	$tmdb = new TMDB($apikey, 'en', true);
	
    //Connection to the database
	include($_SERVER['DOCUMENT_ROOT']."/testsite/api/dbconnection/connection.php");
	
	//Query
	$get = "SELECT * FROM mv_det WHERE mv_id = ".$posted->mvid;
	$result = mysqli_query($conn, $get);
	
	//Respose
	$resp_movie = array();
	$resp_movie['Status'] = false;		 //<- False if no movie exists else true
	$resp_movie['Movie'] = array();		 //<- Store movie info
	
	//Fetch Data
	if (mysqli_num_rows($result) > 0) {
		$row = mysqli_fetch_assoc($result);
		
		$resp_movie['Status'] = true;
		$resp_movie['Movie']['ID'] = $row["mv_id"];
		$resp_movie['Movie']['Name'] = $row["mv_name"];
		$resp_movie['Movie']['Actors'] = $row["mv_act"];
		$resp_movie['Movie']['RDate'] = $row["mv_rdte"];
		$resp_movie['Movie']['AvgVotes'] = $row["mv_avgvote"];
		$resp_movie['Movie']['Runtime'] = $row["mv_runtime"];
		$resp_movie['Movie']['Genre'] = $row["mv_genre"];
		$resp_movie['Movie']['DVDNO'] = $row["mv_dvdno"];
		//$resp_movie['Movie']['Plot'] = $row["mv_plot"];
		$movie = $tmdb->getMovie($row["mv_id"]);
		$resp_movie['Movie']['Plot'] = $movie->getOverview();
		
		$resp_movie['Movie']['FCover'] = $tmdb->getImageURL('original'). $movie->getPoster();
		$resp_movie['Movie']['BCover'] = $tmdb->getImageURL('original'). $movie->getBackdrop();
		
		$resp_movie['Movie']['Trailer'] = $movie->getTrailer();
		
		//Clear cache_movie table
		$empty = "TRUNCATE TABLE mv_cache";
		mysqli_query($conn, $empty);
		
		//Cache last movie select on the list
		$cache_movie = $row["mv_id"];
		$set = "INSERT INTO mv_cache VALUES ('$cache_movie')";
		$result = mysqli_query($conn, $set);
		
	}
	
	//Send Response
	echo json_encode($resp_movie);
	
	
	
	
?>