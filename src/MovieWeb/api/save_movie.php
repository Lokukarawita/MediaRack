<?php
	$posted = json_decode(file_get_contents("php://input"));
	header('Content-Type: application/json');
	include($_SERVER['DOCUMENT_ROOT']."/testsite/api/tmdb-api.php");
	
	$movieid = $posted->movieid;
	$moviename = $posted->moviename;
	$movieactors = $posted->movieactors;
	$movievotes = $posted->moovievotes;
	$movierdate = $posted->movierdate;
	$movieruntime = $posted->movieruntime;
	$moviegenere = $posted->moviegenere;
	
	/*Messages*/
	$record;
	
	//Connection to the database
	include($_SERVER['DOCUMENT_ROOT']."mv_db/api/dbconnection/connection.php");
	
	//Inser Data
	$sql = "INSERT INTO mv_det (mv_id, mv_name, mv_act, mv_avgvote, mv_rdte, mv_runtime, mv_genre) VALUES ('$movieid', '$moviename', '$movieactors', '$movievotes', '$movierdate', '$movieruntime', '$moviegenere')";

		if (mysqli_query($conn, $sql)) {
			//echo "New record created successfully";
			$record = "Movie saved to the database";
		} else {
			//echo "Error: " . $sql . "<br>" . mysqli_error($conn);
			$record = "Movie already saved in the database.";
		}
	$conn->close();
	
	//Response
	$resp_movie = array();

		//$resp_movie['con_message'] = $con_message;
		$resp_movie['record'] = $record;
	
	//Send Response
	echo json_encode($resp_movie);

?>