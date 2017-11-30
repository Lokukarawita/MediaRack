<!DOCTYPE html>
<html>
    <head>
        <link href='http://fonts.googleapis.com/css?family=Montserrat:400,700' rel='stylesheet' type='text/css'/>

        <!--jQuery-->
        <script src="https://code.jquery.com/jquery-2.1.4.min.js"></script>

        <!--Bootstrap JS-->
        <!--<script src="js/bootstrap.min.js"></script>-->

        <!--Bootstrap JS 4-->
        <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.3/umd/popper.min.js" integrity="sha384-vFJXuSJphROIrBnz7yo7oB41mKfc8JzQZiCq4NCceLEaO4IHwicKwpJf9c9IpFgh" crossorigin="anonymous"></script>
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/js/bootstrap.min.js" integrity="sha384-alpBpkh1PFOepccYVYDB4do5UnbKysX5WZXm3XxPqe5iKTfUKjNkCk9SaVuEZflJ" crossorigin="anonymous"></script>

        <!-- Bootstrap Core CSS -->
        <!--<link href="css/bootstrap.min.css" rel="stylesheet">-->

        <!-- Bootstrap Core CSS 4 -->
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0-beta.2/css/bootstrap.min.css" integrity="sha384-PsH8R72JQ3SOdhVi3uxftmaW6Vc51MKb0q5P2rRUpPvrszuE4W1povHYgTpBfshb" crossorigin="anonymous">

        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.5.0/css/font-awesome.min.css"/>

        <!--Flip CDN-->    
        <script src="https://cdn.rawgit.com/nnattawat/flip/master/dist/jquery.flip.min.js"></script>

        <!--Iconicons-->
        <link href="http://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" rel='stylesheet' type='text/css'/>

        <meta charset="UTF-8">

        <title>My Movie Database</title>
        <style>
            body {
                /*background: url('images/login_background.jpg');*/
                background-size: contain;
                font-family: Montserrat;
                margin: inherit;

            }

            .logo {
                width: 427px;
                height: 114px;
                /*background: url('images/logo.png') no-repeat;*/
                margin: 30px auto;
                background-size: contain;
            }

            .login-block {
                width: 450px;
                padding: 39px 25px 60px 25px;
                padding: 20px;
                background: #fff;
                /*border-radius: 5px;*/
                /*border-top: 5px solid #ff656c;*/
                margin: 0 auto;
            }

            .login-block h1 {
                text-align: center;
                color: #000;
                font-size: 18px;
                text-transform: uppercase;
                margin-top: 0;
                margin-bottom: 20px;
            }

            .login-block input {
                width: 100%;
                height: 42px;
                box-sizing: border-box;
                border-radius: 1px;
                border: 1px solid #ccc;
                margin-bottom: 20px;
                font-size: 14px;
                font-family: Montserrat;
                padding: 0 20px 0 50px;
                outline: none;
            }

            .login-block input#username {
                background: #fff url('http://i.imgur.com/u0XmBmv.png') 20px top no-repeat;
                background-size: 16px 80px;
            }

            .login-block input#username:focus {
                background: #fff url('http://i.imgur.com/u0XmBmv.png') 20px bottom no-repeat;
                background-size: 16px 80px;
            }

            .login-block input#password {
                background: #fff url('http://i.imgur.com/Qf83FTt.png') 20px top no-repeat;
                background-size: 16px 80px;
            }

            .login-block input#password:focus {
                background: #fff url('http://i.imgur.com/Qf83FTt.png') 20px bottom no-repeat;
                background-size: 16px 80px;
            }

            .login-block input:active, .login-block input:focus {
                border: 1px solid #ff656c;
            }

            .login-block button {
                width: 100%;
                height: 40px;
                background: #009688!important;
                box-sizing: border-box;
                border-radius: 1px;
                border: 1px solid #018276!important;
                color: #fff;
                font-weight: bold;
                /*text-transform: uppercase;*/
                font-size: 14px;
                font-family: Montserrat;
                outline: none;
                cursor: pointer;
            }

            .login-block button:hover {
                background: #383737;
            }

            .error{
                margin-top:10px !important;
            }

            .back-mask{
                background-color: rgba(0, 0, 0, 0.68);
                height: -webkit-fill-available;
                display: flow-root;
            }

            .btn_submit{
                margin-bottom: 15px;
            }

            .flip_btn{
                cursor:pointer;
                color: #5a5757;
                font-size: 14px;
            }

            .alert{
                margin-top: 30px;
                display: none;
                text-align: center;
            }
            .alert span{
                vertical-align: bottom;
            }
            .alert i{
                vertical-align: middle;
            }
            .ionicons{
                font-size: 27px;
                margin-right: 10px;
            }

        </style>
    </head>

    <body>
        <div class="container-fluid">
            <div class="row">

                <div class="container-fluid back-mask">
                    <div class="logo"></div>
                    <div id="card"> 
                        <div class="front"> 
                            <!--Signin Form-->
                            <div class="login-block">
                                <form>
                                    <!--<h1>Signin</h1>-->
                                    <input type="text" name="username" value="" placeholder="Username" id="sin_username" />
                                    <input type="password" name="password" value="" placeholder="Password" id="sin_password" />
                                    <button class="btn_submit" id="btn_signin" type="button" value="submit">Sign In</button>
                                    <span class="flip_btn">Sign Up &#8646;</span>
                                </form>
                                <div id="signin_alert" class="alert alert-danger" role="alert">
                                    <i class="ionicons ion-information-circled"></i><span></span>
                                </div>    
                            </div>
                        </div> 
                        <div class="back">
                            <!--Signup form-->
                            <div class="login-block">
                                <form>
                                    <!--<h1>Signup</h1>-->
                                    <input type="text" name="username" value="" placeholder="Username" id="sup_username" />
                                    <input type="password" name="password" value="" placeholder="Password" id="sup_password" />
                                    <input type="email" name="email" value="" placeholder="Email" id="sup_email" />
                                    <button class="btn_submit" id="btn_signup" class="btn_submit" type="button" value="submit">Sign Up</button>
                                    <span class="flip_btn">Sign In &#8646;</span>
                                </form>
                                <div id="signup_alert" class="alert alert-success" role="alert">
                                    <i class="ionicons ion-information-circled"></i> <span></span>
                                </div> 
                            </div>
                        </div>   
                    </div>
                </div>
            </div>
        </div>
        <script>
            //Flip card js
            $("#card").flip({
                trigger: 'manual'
            });
            $(".flip_btn").click(function(){
                $("#card").flip('toggle');
            });

            //Sign up users
            $("#btn_signup").click(function(){

                var usname = $("#sup_username").val();
                var pword = $("#sup_password").val();
                var email = $("#sup_email").val();

                //  alert(usname+pword);

                var objX = {
                    "action": 'signup',
                    "username": usname,
                    "password": pword,
                    "email": email
                };

                $.ajax({
                    type: 'POST',
                    url: '/testsite/login.php',
                    data: JSON.stringify(objX),
                    contentType: "application/json",
                    dataType: "json",
                    success: function (r) {
                        alert(r);
                    },error:function(x,y,z){
                        alert("error");
                    }
                });
            });

            //Signin Users
            $("#btn_signin").click(function(){
                var usname = $("#sin_username").val();
                var pword = $("#sin_password").val();

                var objX = {
                    "action": 'signin',
                    "username": usname,
                    "password": pword
                };

                $.ajax({
                    type: 'POST',
                    url: '/testsite/login.php',
                    data: JSON.stringify(objX),
                    contentType: "application/json",
                    dataType: "json",
                    success: function (r) {
                        if(r=="Exists"){
                            window.location.href = 'home.php';
                        }else if(r=="Deactive"){
                            jQuery("#signin_alert").show();
                            jQuery("#signin_alert>span").text("Your account has been suspended");
                        }else if(r=="Nouser"){
                            jQuery("#signin_alert").show();
                            jQuery("#signin_alert>span").text("Invalid username or password");
                        }
                    },error:function(x,y,z){
                        alert("error");
                    }
                });
            });


        </script>
    </body>

</html>