<?php
session_start();

$posted = json_decode(file_get_contents("php://input"));
header('Content-Type: application/json');

//Connection to the database
include($_SERVER['DOCUMENT_ROOT']."/testsite/api/dbconnection/connection.php");

$action = $posted->action;
$uname = $posted->username;
$pword = $posted->password;
$pwrdmd5 = md5($pword);

//Signup users to the app
if($action=="signup"){

    $email = $posted->email;

    //Create user
    $sql = "INSERT INTO users(Username,Password,Email,Status) VALUES('$uname','$pwrdmd5','$email','Active')";

    if (mysqli_query($conn, $sql)) {
        //echo "New record created successfully";
        //$record = "User has been created to the system";

        $getid = "SELECT Id FROM users WHERE Email='$email'";

        $result = mysqli_query($conn, $getid);
        if (mysqli_num_rows($result) > 0) {
            while($row = mysqli_fetch_assoc($result)) {
                $tblname = "mr_rack".$row["Id"];

                $newtbl = "CREATE TABLE $tblname 
                    (Id INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
                    Watched INT(11),
                    WatchedOn DATETIME,
                    Favorite INT(11),
                    Grade VARCHAR(100),
                    Comment VARCHAR(500),
                    Bookmark VARCHAR(100),
                    ImageCacheID VARCHAR(50),
                    Timestamp TIMESTAMP,
                    IDInfo LONGTEXT,
                    CompositionInfo LONGTEXT,
                    FileInfo LONGTEXT)";

                if ($conn->query($newtbl) === TRUE) {
                    $record = "Account has been created successfully";
                } else {
                    $record = "Error creating table: " . $conn->error;
                }
            }
        }

    } else {
        //echo "Error: " . $sql . "<br>" . mysqli_error($conn);
        $record = "User already saved in the system";
    }
    $conn->close();

    echo json_encode($record);
}

//Signin users to the app
else if($action=="signin"){

    $get = "SELECT * FROM users";
    $result = mysqli_query($conn, $get);

    if (mysqli_num_rows($result) > 0) {
        // output data of each row
        while($row = mysqli_fetch_assoc($result)) {
            if($uname == $row["Username"] &&  $pwrdmd5 == $row["Password"] && "Active" == $row["Status"]){

                $_SESSION["username"] = $uname;
                $_SESSION["password"] = $pwrdmd5;

                $getid = "SELECT Id FROM users WHERE Username='$uname'";

                $result = mysqli_query($conn, $getid);
                if (mysqli_num_rows($result) > 0) {
                    while($row = mysqli_fetch_assoc($result)) {
                        //Save userid to session storage
                        $_SESSION["userid"] = $row["Id"];
                    }
                }

                $record = "Exists";
                //$record =  $_SESSION["userid"];
                break;
            }else if($uname == $row["Username"] &&  $pwrdmd5 == $row["Password"] && "Deactive" == $row["Status"]){

                //When user is deactivated in the system
                $record = "Deactive";
                break;
            }else{

                //When user is not available in the system
                $record = "Nouser";
            }
        }

        $conn->close();

        echo json_encode($record);
    }
}

?>