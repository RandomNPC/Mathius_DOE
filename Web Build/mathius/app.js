module.exports=function() {
	/*
	 * Module dependencies.
	 */
	var express=require('express');
	var http=require('http');
	
	var app=express();
	
	function Score(username,score){
		this.username = username;
		this.score = score;
	}

	app.configure(function() {
		app.set('port', process.env.PORT||3000);
		app.set('views', __dirname+'/views');
		app.engine('html', require('ejs').renderFile);
		app.use(express.bodyParser());
		app.use(express.methodOverride());
		app.use('/', express.static(__dirname+"/views/"));
	});

	app.get('/', function(req, res) {
		res.render('mathius.html');
	});
	

	http.createServer(app).listen(app.get('port'), function() {
		console.log('Express server listening on port '+app.get('port'));
	});

};