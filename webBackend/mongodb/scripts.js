new Mongo();
conn = new Mongo();
db = conn.getDB("uitDatabase");
db.createCollection("Users");

load("classes.js");
load("lessons.js");
load("chapters.js");
load("issues.js");
load("answers.js");
load("group.js");
load("schedule.js");
load("project.js");
load("Question.js");
