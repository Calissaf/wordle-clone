CREATE DATABASE IF NOT EXISTS wordList;

USE wordList;

CREATE TABLE words (
    id INT PRIMARY KEY AUTO_INCREMENT,
    word VARCHAR(5) NOT NULL
);

INSERT INTO words (word)
VALUES 
('aback'),
('abase'),
('abate'),
('abbey'),
('abbot');

SELECT *
FROM words;

CREATE TABLE allowed_words (
    id INT PRIMARY KEY AUTO_INCREMENT,
    word VARCHAR(5) NOT NULL
);

SHOW TABLES;

ALTER TABLE allowed_words 
ADD CONSTRAINT UQ_word UNIQUE(word);

ALTER TABLE words 
ADD CONSTRAINT UQ_word UNIQUE(word);

DESCRIBE allowed_words;
DESCRIBE words;

SHOW GLOBAL VARIABLES LIKE 'local_infile';
SET GLOBAL local_infile = 'ON';

LOAD DATA LOCAL INFILE 'C:/Users/cfairburn/Documents/wordle-answers-alphabetical.txt' INTO TABLE allowed_words(word);

SELECT COUNT(id) FROM words;

SELECT word FROM words WHERE id = 3;

SELECT id FROM allowed_words WHERE word = 'aback';
