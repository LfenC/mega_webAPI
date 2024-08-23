CREATE DATABASE mega_database;

USE mega_database;

CREATE TABLE users (
	user_id INT PRIMARY KEY IDENTITY(1,1),
	username NVARCHAR(50) NOT NULL,
	email NVARCHAR(45),
	password NVARCHAR(45),
	register_date DATETIME DEFAULT GETDATE(),
	first_name NVARCHAR(45),
	last_name NVARCHAR(45)
);

CREATE TABLE movies (
	movie_id INT PRIMARY KEY IDENTITY(1,1),
	title NVARCHAR(255) NOT NULL,
	description NVARCHAR (MAX),
	release_date DATE,
	rating FLOAT,
	votes INT,
	image_url NVARCHAR(255),
	video_url NVARCHAR(255),
	added_date DATETIME DEFAULT GETDATE()
);

CREATE TABLE tvshows (
	tvshow_id INT PRIMARY KEY IDENTITY(1,1),
	title NVARCHAR(255) NOT NULL,
	description NVARCHAR (MAX),
	release_date DATE,
	rating FLOAT DEFAULT 0.0,
	votes INT DEFAULT 0,
	image_url NVARCHAR(255),
	video_url NVARCHAR(255),
	added_date DATETIME DEFAULT GETDATE()
);

CREATE TABLE episodes (
	episode_id INT PRIMARY KEY IDENTITY(1,1),
	tvshow_id INT NOT NULL FOREIGN KEY REFERENCES tvshows(tvshow_id),
	title NVARCHAR(255) NOT NULL,
	description NVARCHAR (MAX),
	release_date DATE,
	duration INT,
	episode_number INT,
	season_number INT,
	video_url NVARCHAR(255),
);

CREATE TABLE genres (
	genre_id INT PRIMARY KEY IDENTITY(1,1),
	name NVARCHAR(50) NOT NULL UNIQUE,
);

CREATE TABLE moviesgenres (
	movie_id INT NOT NULL FOREIGN KEY REFERENCES movies(movie_id),
	genre_id INT NOT NULL FOREIGN KEY REFERENCES genres(genre_id),
	PRIMARY KEY (movie_id, genre_id)
);

CREATE TABLE tvshowsgenres (
	tvshow_id INT NOT NULL FOREIGN KEY REFERENCES tvshows(tvshow_id),
	genre_id INT NOT NULL FOREIGN KEY REFERENCES genres(genre_id),
	PRIMARY KEY (tvshow_id, genre_id)
);

/*categories to have toprating, popular, upcoming movies or tv shows*/

CREATE TABLE categories (
	category_id INT PRIMARY KEY IDENTITY(1,1),
	name NVARCHAR(50) NOT NULL UNIQUE,
);

CREATE TABLE moviescategories(
	movie_id INT NOT NULL FOREIGN KEY REFERENCES movies(movie_id),
	category_id INT NOT NULL FOREIGN KEY REFERENCES categories(category_id),
	PRIMARY KEY (movie_id, category_id)
);

CREATE TABLE tvshowscategories(
	tvshow_id INT NOT NULL FOREIGN KEY REFERENCES tvshows(tvshow_id),
	category_id INT NOT NULL FOREIGN KEY REFERENCES categories(category_id),
	PRIMARY KEY (tvshow_id, category_id)
);


CREATE TABLE favorites_movies (
	favorite_id INT PRIMARY KEY IDENTITY(1,1),
	user_id INT NOT NULL FOREIGN KEY REFERENCES users(user_id),
	movie_id INT NOT NULL FOREIGN KEY REFERENCES movies(movie_id),
	added_date DATETIME DEFAULT GETDATE()
);

CREATE TABLE favorites_tvshows (
	favorite_id INT PRIMARY KEY IDENTITY(1,1),
	user_id INT NOT NULL FOREIGN KEY REFERENCES users(user_id),
	tvshows_id INT NOT NULL FOREIGN KEY REFERENCES tvshows(tvshow_id),
	added_date DATETIME DEFAULT GETDATE()
);


CREATE TABLE reviews (
	review_id INT PRIMARY KEY IDENTITY(1,1),
	user_id INT NOT NULL FOREIGN KEY REFERENCES users(user_id),
	movie_id INT FOREIGN KEY REFERENCES movies(movie_id),
	tvshows_id INT FOREIGN KEY REFERENCES tvshows(tvshow_id),
	/*check with the rating to update*/
	rating INT CHECK (rating >= 1 AND rating <=5),
	review_text NVARCHAR (MAX),
	review_date DATETIME DEFAULT GETDATE()
);

CREATE TABLE actors (
	actor_id INT PRIMARY KEY IDENTITY(1,1),
	first_name NVARCHAR(50) NOT NULL,
	last_name NVARCHAR(50) NOT NULL,
	birth_date DATE,
	picture_url NVARCHAR(255)
);

CREATE TABLE moviesactors (
	movie_id INT NOT NULL FOREIGN KEY REFERENCES movies(movie_id),
	actor_id INT NOT NULL FOREIGN KEY REFERENCES actors(actor_id),
	role NVARCHAR(100),
	PRIMARY KEY (movie_id, actor_id)
);

CREATE TABLE tvshowsactors (
	tvshow_id INT NOT NULL FOREIGN KEY REFERENCES tvshows(tvshow_id),
	actor_id INT NOT NULL FOREIGN KEY REFERENCES actors(actor_id),
	role NVARCHAR(100),
	PRIMARY KEY (tvshow_id, actor_id)
);

CREATE TABLE directors (
	director_id INT PRIMARY KEY IDENTITY(1,1),
	first_name NVARCHAR (50) NOT NULL,
	last_name NVARCHAR(50) NOT NULL,
	picture_url NVARCHAR (255)
);

CREATE TABLE moviesdirectors (
	movie_id INT NOT NULL FOREIGN KEY REFERENCES movies(movie_id),
	director_id INT NOT NULL FOREIGN KEY REFERENCES directors(director_id),
	PRIMARY KEY (movie_id, director_id)
);

CREATE TABLE tvshowsdirectors (
	tvshow_id INT NOT NULL FOREIGN KEY REFERENCES tvshows(tvshow_id),
	director_id INT NOT NULL FOREIGN KEY REFERENCES directors(director_id),
	PRIMARY KEY (tvshow_id, director_id)
);

CREATE TABLE moviesvotes(
	vote_id INT PRIMARY KEY IDENTITY(1,1),
	movie_id INT NOT NULL FOREIGN KEY REFERENCES movies(movie_id),
	user_id INT NOT NULL FOREIGN KEY REFERENCES users(user_id),
	rating INT CHECK (rating >=1 AND rating <=5),
	vote_date DATETIME DEFAULT GETDATE()
);

CREATE TABLE tvshowsvotes(
	vote_id INT PRIMARY KEY IDENTITY(1,1),
	tvshow_id INT NOT NULL FOREIGN KEY REFERENCES tvshows(tvshow_id),
	user_id INT NOT NULL FOREIGN KEY REFERENCES users(user_id),
	rating INT CHECK (rating >=1 AND rating <=5),
	vote_date DATETIME DEFAULT GETDATE()
);


/*how to update the rating once the person enters his/her vote?*/
/*for movies*/

CREATE TRIGGER trig_UpdateMoviesRating
ON moviesvotes
AFTER INSERT
AS
BEGIN 
	SET NOCOUNT ON;
	UPDATE movies
	SET votes = mv.votes,
		rating = mv.rating
	FROM movies m
	INNER JOIN 
		(SELECT movie_id, COUNT(*) AS votes,
						AVG(rating) AS rating
		FROM
			moviesvotes
		WHERE
			movie_id IN (SELECT movie_id FROM inserted)
		GROUP BY
			movie_id) mv
		ON 
			m.movie_id = mv.movie_id;
END;

/*aqui me quedé*/
/*for tvshows*/

CREATE TRIGGER trig_UpdateTvshowsRating
ON tvshowsvotes
AFTER INSERT
AS
BEGIN 
	SET ONCOUNT ON;
	DECLARE @tvshow_id INT;
	SELECT @tvshow_id = inserted.tvshow_id FROM inserted;

	UPDATE tvshows
	SET votes = (SELECT COUNT(*) FROM tvshowsvotes WHERE tvshow_id = @tvshow_id),
		rating = (SELECT AVG(rating) FROM moviesvotes WHERE tvshow_id = @tvshow_id)
	WHERE tvshow_id = @tvshow_id;
END;

/*Insert categories used in the json file*/
INSERT INTO categories (name) VALUES ('Featured');
INSERT INTO categories (name) VALUES ('Upcoming');
INSERT INTO categories (name) VALUES ('Top Rated');
INSERT INTO categories (name) VALUES ('Popular');

/* how to give to the user recommendations based on what he/she has seen before?
... can create a procedure for that?*/
/* first create the table for the viewing history */
CREATE TABLE viewinghistory (
	vhistory_id INT PRIMARY KEY IDENTITY (1,1),
	user_id INT NOT NULL FOREIGN KEY REFERENCES users(user_id),
	movie_id INT FOREIGN KEY REFERENCES movies(movie_id),
	tvshow_id INT FOREIGN KEY REFERENCES tvshows(tvshow_id),
	view_date DATETIME DEFAULT GETDATE()
);

CREATE VIEW UserWatchedMovieGenres AS
SELECT 
	vh.user_id, 
	mg.genre_id,
	g.name AS genre_name
FROM 
	viewinghistory vh
JOIN 
	movies m ON vh.movie_id = m.movie_id
JOIN 
	moviesgenres mg ON m.movie_id = mg.movie_id
JOIN 
	genres g ON mg.genre_id = g.genre_id
WHERE 
	vh.movie_id IS NOT NULL;


CREATE VIEW UserWatchedTvshowGenres AS
SELECT 
	vh.user_id, 
	tg.genre_id,
	g.name AS genre_name
FROM 
	viewinghistory vh
JOIN 
	tvshows t ON vh.tvshow_id = t.tvshow_id
JOIN 
	tvshowsgenres tg ON t.tvshow_id = tg.tvshow_id
JOIN 
	genres g ON tg.genre_id = g.genre_id
WHERE 
	vh.tvshow_id IS NOT NULL;

/* one we create de views we can create the procedure to give them*/

CREATE PROCEDURE RecommendMovies
	@user_id INT
AS
BEGIN 
	SET NOCOUNT ON; 
	SELECT DISTINCT 
		m.movie_id,
		m.title,
		m.description, 
		m.release_date, 
		m.rating,
		m.votes,
		m.image_url,
		m.video_url,
		m.added_date
	FROM 
		movies m
	JOIN 
		moviesgenres mg ON m.movie_id = mg.movie_id
	JOIN 
		genres g ON mg.genre_id = g.genre_id
	JOIN 
		UserWatchedMovieGenres uwm ON g.genre_id = uwm.genre_id
	WHERE
		uwm.user_id = @user_id
		AND m.movie_id NOT IN (SELECT movie_id FROM UserWatchedMovieGenres WHERE user_id = @user_id)
	ORDER BY 
		m.rating DESC, m.votes DESC,m.release_date DESC; 
END;

/* how can i give recommendations base on the serie o tv show the user is currently watching?*/

CREATE TABLE currentwatching (
	user_id INT NOT NULL FOREIGN KEY REFERENCES users(user_id),
	movie_id INT FOREIGN KEY REFERENCES movies(movie_id),
	tvshow_id INT FOREIGN KEY REFERENCES tvshows(tvshow_id),
	PRIMARY KEY (user_id)
);

CREATE PROCEDURE RecommendSimiliarMovies 
	@user_id INT
AS
BEGIN 
	SET NOCOUNT ON;

	DECLARE @current_movie_id INT;
	SELECT @current_movie_id = movie_id
	FROM currentwatching
	WHERE user_id = @user_id;

	IF @current_movie_id IS NOT NULL
	BEGIN
		SELECT DISTINCT 
			m.movie_id,
			m.title,
			m.description, 
			m.release_date, 
			m.rating,
			m.votes,
			m.image_url,
			m.video_url,
			m.added_date
		FROM 
			movies m
		JOIN 
			moviesgenres mg ON m.movie_id = mg.movie_id
		JOIN 
			(SELECT genre_id FROM moviesgenres WHERE movie_id = @current_movie_id) AS cg ON mg.genre_id = cg.genre_id
		WHERE
			m.movie_id <> @current_movie_id
		ORDER BY 
			m.rating DESC, m.votes DESC, m.release_date DESC;
	END
	ELSE 
	BEGIN 
		PRINT('')
	END
END;
