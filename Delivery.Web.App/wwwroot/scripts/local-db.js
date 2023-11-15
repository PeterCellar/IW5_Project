let db;

window.LocalDb = {
  Initialize: function () {
    db = new Dexie("delivery_database");
    db.version(1).stores({
      dishes: "id",
      restaurants: "id",
      orders: "id",
    });
  },
  GetAll: async function (tableName) {
    return await db.table(tableName).toArray();
  },
  GetById: async function (id) {
    let result = await db.table(tableName).get(id);
    return result;
  },
  Insert: function (tableName, entity) {
    db.table(tableName).put(entity);
  },
  Remove: async function (tableName, id) {
    await db.table(tableName).bulkDelete([id]);
  },
};
