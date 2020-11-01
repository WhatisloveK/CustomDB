// package: dbEngine
// file: src/app/protos/dbEngine.proto

import * as src_app_protos_dbEngine_pb from "../generated/dbEngine_pb";
import {grpc} from "@improbable-eng/grpc-web";

type DBServiceCreateDatabase = {
  readonly methodName: string;
  readonly service: typeof DBService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbEngine_pb.CreateDbRequest;
  readonly responseType: typeof src_app_protos_dbEngine_pb.BaseReply;
};

type DBServiceCreateTable = {
  readonly methodName: string;
  readonly service: typeof DBService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbEngine_pb.TableRequest;
  readonly responseType: typeof src_app_protos_dbEngine_pb.BaseReply;
};

type DBServiceDeleteTable = {
  readonly methodName: string;
  readonly service: typeof DBService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbEngine_pb.TableRequest;
  readonly responseType: typeof src_app_protos_dbEngine_pb.BaseReply;
};

type DBServiceGetTableList = {
  readonly methodName: string;
  readonly service: typeof DBService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbEngine_pb.GetTableListRequest;
  readonly responseType: typeof src_app_protos_dbEngine_pb.GetTableListReply;
};

export class DBService {
  static readonly serviceName: string;
  static readonly CreateDatabase: DBServiceCreateDatabase;
  static readonly CreateTable: DBServiceCreateTable;
  static readonly DeleteTable: DBServiceDeleteTable;
  static readonly GetTableList: DBServiceGetTableList;
}

type EntityServiceAddColumn = {
  readonly methodName: string;
  readonly service: typeof EntityService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbEngine_pb.AddColumnRequest;
  readonly responseType: typeof src_app_protos_dbEngine_pb.BaseReply;
};

type EntityServiceDropColumn = {
  readonly methodName: string;
  readonly service: typeof EntityService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbEngine_pb.DropColumnRequst;
  readonly responseType: typeof src_app_protos_dbEngine_pb.BaseReply;
};

type EntityServiceInsert = {
  readonly methodName: string;
  readonly service: typeof EntityService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbEngine_pb.InsertRequest;
  readonly responseType: typeof src_app_protos_dbEngine_pb.BaseReply;
};

type EntityServiceDelete = {
  readonly methodName: string;
  readonly service: typeof EntityService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbEngine_pb.DeleteRequest;
  readonly responseType: typeof src_app_protos_dbEngine_pb.BaseReply;
};

type EntityServiceUpdate = {
  readonly methodName: string;
  readonly service: typeof EntityService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbEngine_pb.UpdateRequest;
  readonly responseType: typeof src_app_protos_dbEngine_pb.BaseReply;
};

type EntityServiceSelect = {
  readonly methodName: string;
  readonly service: typeof EntityService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbEngine_pb.SelectRequest;
  readonly responseType: typeof src_app_protos_dbEngine_pb.SelectReply;
};

type EntityServiceInnerJoin = {
  readonly methodName: string;
  readonly service: typeof EntityService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbEngine_pb.InnerJoinRequest;
  readonly responseType: typeof src_app_protos_dbEngine_pb.SelectReply;
};

type EntityServiceCrossJoin = {
  readonly methodName: string;
  readonly service: typeof EntityService;
  readonly requestStream: false;
  readonly responseStream: false;
  readonly requestType: typeof src_app_protos_dbEngine_pb.CrossJoinRequest;
  readonly responseType: typeof src_app_protos_dbEngine_pb.SelectReply;
};

export class EntityService {
  static readonly serviceName: string;
  static readonly AddColumn: EntityServiceAddColumn;
  static readonly DropColumn: EntityServiceDropColumn;
  static readonly Insert: EntityServiceInsert;
  static readonly Delete: EntityServiceDelete;
  static readonly Update: EntityServiceUpdate;
  static readonly Select: EntityServiceSelect;
  static readonly InnerJoin: EntityServiceInnerJoin;
  static readonly CrossJoin: EntityServiceCrossJoin;
}

export type ServiceError = { message: string, code: number; metadata: grpc.Metadata }
export type Status = { details: string, code: number; metadata: grpc.Metadata }

interface UnaryResponse {
  cancel(): void;
}
interface ResponseStream<T> {
  cancel(): void;
  on(type: 'data', handler: (message: T) => void): ResponseStream<T>;
  on(type: 'end', handler: (status?: Status) => void): ResponseStream<T>;
  on(type: 'status', handler: (status: Status) => void): ResponseStream<T>;
}
interface RequestStream<T> {
  write(message: T): RequestStream<T>;
  end(): void;
  cancel(): void;
  on(type: 'end', handler: (status?: Status) => void): RequestStream<T>;
  on(type: 'status', handler: (status: Status) => void): RequestStream<T>;
}
interface BidirectionalStream<ReqT, ResT> {
  write(message: ReqT): BidirectionalStream<ReqT, ResT>;
  end(): void;
  cancel(): void;
  on(type: 'data', handler: (message: ResT) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'end', handler: (status?: Status) => void): BidirectionalStream<ReqT, ResT>;
  on(type: 'status', handler: (status: Status) => void): BidirectionalStream<ReqT, ResT>;
}

export class DBServiceClient {
  readonly serviceHost: string;

  constructor(serviceHost: string, options?: grpc.RpcOptions);
  createDatabase(
    requestMessage: src_app_protos_dbEngine_pb.CreateDbRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.BaseReply|null) => void
  ): UnaryResponse;
  createDatabase(
    requestMessage: src_app_protos_dbEngine_pb.CreateDbRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.BaseReply|null) => void
  ): UnaryResponse;
  createTable(
    requestMessage: src_app_protos_dbEngine_pb.TableRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.BaseReply|null) => void
  ): UnaryResponse;
  createTable(
    requestMessage: src_app_protos_dbEngine_pb.TableRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.BaseReply|null) => void
  ): UnaryResponse;
  deleteTable(
    requestMessage: src_app_protos_dbEngine_pb.TableRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.BaseReply|null) => void
  ): UnaryResponse;
  deleteTable(
    requestMessage: src_app_protos_dbEngine_pb.TableRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.BaseReply|null) => void
  ): UnaryResponse;
  getTableList(
    requestMessage: src_app_protos_dbEngine_pb.GetTableListRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.GetTableListReply|null) => void
  ): UnaryResponse;
  getTableList(
    requestMessage: src_app_protos_dbEngine_pb.GetTableListRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.GetTableListReply|null) => void
  ): UnaryResponse;
}

export class EntityServiceClient {
  readonly serviceHost: string;

  constructor(serviceHost: string, options?: grpc.RpcOptions);
  addColumn(
    requestMessage: src_app_protos_dbEngine_pb.AddColumnRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.BaseReply|null) => void
  ): UnaryResponse;
  addColumn(
    requestMessage: src_app_protos_dbEngine_pb.AddColumnRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.BaseReply|null) => void
  ): UnaryResponse;
  dropColumn(
    requestMessage: src_app_protos_dbEngine_pb.DropColumnRequst,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.BaseReply|null) => void
  ): UnaryResponse;
  dropColumn(
    requestMessage: src_app_protos_dbEngine_pb.DropColumnRequst,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.BaseReply|null) => void
  ): UnaryResponse;
  insert(
    requestMessage: src_app_protos_dbEngine_pb.InsertRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.BaseReply|null) => void
  ): UnaryResponse;
  insert(
    requestMessage: src_app_protos_dbEngine_pb.InsertRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.BaseReply|null) => void
  ): UnaryResponse;
  delete(
    requestMessage: src_app_protos_dbEngine_pb.DeleteRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.BaseReply|null) => void
  ): UnaryResponse;
  delete(
    requestMessage: src_app_protos_dbEngine_pb.DeleteRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.BaseReply|null) => void
  ): UnaryResponse;
  update(
    requestMessage: src_app_protos_dbEngine_pb.UpdateRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.BaseReply|null) => void
  ): UnaryResponse;
  update(
    requestMessage: src_app_protos_dbEngine_pb.UpdateRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.BaseReply|null) => void
  ): UnaryResponse;
  select(
    requestMessage: src_app_protos_dbEngine_pb.SelectRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.SelectReply|null) => void
  ): UnaryResponse;
  select(
    requestMessage: src_app_protos_dbEngine_pb.SelectRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.SelectReply|null) => void
  ): UnaryResponse;
  innerJoin(
    requestMessage: src_app_protos_dbEngine_pb.InnerJoinRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.SelectReply|null) => void
  ): UnaryResponse;
  innerJoin(
    requestMessage: src_app_protos_dbEngine_pb.InnerJoinRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.SelectReply|null) => void
  ): UnaryResponse;
  crossJoin(
    requestMessage: src_app_protos_dbEngine_pb.CrossJoinRequest,
    metadata: grpc.Metadata,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.SelectReply|null) => void
  ): UnaryResponse;
  crossJoin(
    requestMessage: src_app_protos_dbEngine_pb.CrossJoinRequest,
    callback: (error: ServiceError|null, responseMessage: src_app_protos_dbEngine_pb.SelectReply|null) => void
  ): UnaryResponse;
}

