// package: dbEngine
// file: src/app/protos/dbEngine.proto

var src_app_protos_dbEngine_pb = require("../generated/dbEngine_pb");
var grpc = require("@improbable-eng/grpc-web").grpc;

var DBService = (function () {
  function DBService() {}
  DBService.serviceName = "dbEngine.DBService";
  return DBService;
}());

DBService.CreateDatabase = {
  methodName: "CreateDatabase",
  service: DBService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbEngine_pb.CreateDbRequest,
  responseType: src_app_protos_dbEngine_pb.BaseReply
};

DBService.CreateTable = {
  methodName: "CreateTable",
  service: DBService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbEngine_pb.TableRequest,
  responseType: src_app_protos_dbEngine_pb.BaseReply
};

DBService.DeleteTable = {
  methodName: "DeleteTable",
  service: DBService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbEngine_pb.TableRequest,
  responseType: src_app_protos_dbEngine_pb.BaseReply
};

DBService.GetTableList = {
  methodName: "GetTableList",
  service: DBService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbEngine_pb.GetTableListRequest,
  responseType: src_app_protos_dbEngine_pb.GetTableListReply
};

exports.DBService = DBService;

function DBServiceClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

DBServiceClient.prototype.createDatabase = function createDatabase(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(DBService.CreateDatabase, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

DBServiceClient.prototype.createTable = function createTable(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(DBService.CreateTable, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

DBServiceClient.prototype.deleteTable = function deleteTable(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(DBService.DeleteTable, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

DBServiceClient.prototype.getTableList = function getTableList(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(DBService.GetTableList, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

exports.DBServiceClient = DBServiceClient;

var EntityService = (function () {
  function EntityService() {}
  EntityService.serviceName = "dbEngine.EntityService";
  return EntityService;
}());

EntityService.AddColumn = {
  methodName: "AddColumn",
  service: EntityService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbEngine_pb.AddColumnRequest,
  responseType: src_app_protos_dbEngine_pb.BaseReply
};

EntityService.DropColumn = {
  methodName: "DropColumn",
  service: EntityService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbEngine_pb.DropColumnRequst,
  responseType: src_app_protos_dbEngine_pb.BaseReply
};

EntityService.Insert = {
  methodName: "Insert",
  service: EntityService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbEngine_pb.InsertRequest,
  responseType: src_app_protos_dbEngine_pb.BaseReply
};

EntityService.Delete = {
  methodName: "Delete",
  service: EntityService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbEngine_pb.DeleteRequest,
  responseType: src_app_protos_dbEngine_pb.BaseReply
};

EntityService.Update = {
  methodName: "Update",
  service: EntityService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbEngine_pb.UpdateRequest,
  responseType: src_app_protos_dbEngine_pb.BaseReply
};

EntityService.Select = {
  methodName: "Select",
  service: EntityService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbEngine_pb.SelectRequest,
  responseType: src_app_protos_dbEngine_pb.SelectReply
};

EntityService.InnerJoin = {
  methodName: "InnerJoin",
  service: EntityService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbEngine_pb.InnerJoinRequest,
  responseType: src_app_protos_dbEngine_pb.SelectReply
};

EntityService.CrossJoin = {
  methodName: "CrossJoin",
  service: EntityService,
  requestStream: false,
  responseStream: false,
  requestType: src_app_protos_dbEngine_pb.CrossJoinRequest,
  responseType: src_app_protos_dbEngine_pb.SelectReply
};

exports.EntityService = EntityService;

function EntityServiceClient(serviceHost, options) {
  this.serviceHost = serviceHost;
  this.options = options || {};
}

EntityServiceClient.prototype.addColumn = function addColumn(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(EntityService.AddColumn, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

EntityServiceClient.prototype.dropColumn = function dropColumn(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(EntityService.DropColumn, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

EntityServiceClient.prototype.insert = function insert(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(EntityService.Insert, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

EntityServiceClient.prototype.delete = function pb_delete(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(EntityService.Delete, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

EntityServiceClient.prototype.update = function update(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(EntityService.Update, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

EntityServiceClient.prototype.select = function select(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(EntityService.Select, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

EntityServiceClient.prototype.innerJoin = function innerJoin(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(EntityService.InnerJoin, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

EntityServiceClient.prototype.crossJoin = function crossJoin(requestMessage, metadata, callback) {
  if (arguments.length === 2) {
    callback = arguments[1];
  }
  var client = grpc.unary(EntityService.CrossJoin, {
    request: requestMessage,
    host: this.serviceHost,
    metadata: metadata,
    transport: this.options.transport,
    debug: this.options.debug,
    onEnd: function (response) {
      if (callback) {
        if (response.status !== grpc.Code.OK) {
          var err = new Error(response.statusMessage);
          err.code = response.status;
          err.metadata = response.trailers;
          callback(err, null);
        } else {
          callback(null, response.message);
        }
      }
    }
  });
  return {
    cancel: function () {
      callback = null;
      client.close();
    }
  };
};

exports.EntityServiceClient = EntityServiceClient;

