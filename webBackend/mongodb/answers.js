new Mongo()
conn = new Mongo()
db = conn.getDB('uitDatabase')
db.createCollection('Answers')