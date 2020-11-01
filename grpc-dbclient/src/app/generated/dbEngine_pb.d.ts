// package: dbEngine
// file: src/app/protos/dbEngine.proto

import * as jspb from "google-protobuf";

export class GetEntityRequest extends jspb.Message {
  getDbname(): string;
  setDbname(value: string): void;

  getTablename(): string;
  setTablename(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetEntityRequest.AsObject;
  static toObject(includeInstance: boolean, msg: GetEntityRequest): GetEntityRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: GetEntityRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetEntityRequest;
  static deserializeBinaryFromReader(message: GetEntityRequest, reader: jspb.BinaryReader): GetEntityRequest;
}

export namespace GetEntityRequest {
  export type AsObject = {
    dbname: string,
    tablename: string,
  }
}

export class Column extends jspb.Message {
  getName(): string;
  setName(value: string): void;

  getDatavaluetypeid(): string;
  setDatavaluetypeid(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): Column.AsObject;
  static toObject(includeInstance: boolean, msg: Column): Column.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: Column, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): Column;
  static deserializeBinaryFromReader(message: Column, reader: jspb.BinaryReader): Column;
}

export namespace Column {
  export type AsObject = {
    name: string,
    datavaluetypeid: string,
  }
}

export class GetEntityReply extends jspb.Message {
  getCode(): number;
  setCode(value: number): void;

  getMessage(): string;
  setMessage(value: string): void;

  getStacktrace(): string;
  setStacktrace(value: string): void;

  clearColumnsList(): void;
  getColumnsList(): Array<Column>;
  setColumnsList(value: Array<Column>): void;
  addColumns(value?: Column, index?: number): Column;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetEntityReply.AsObject;
  static toObject(includeInstance: boolean, msg: GetEntityReply): GetEntityReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: GetEntityReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetEntityReply;
  static deserializeBinaryFromReader(message: GetEntityReply, reader: jspb.BinaryReader): GetEntityReply;
}

export namespace GetEntityReply {
  export type AsObject = {
    code: number,
    message: string,
    stacktrace: string,
    columnsList: Array<Column.AsObject>,
  }
}

export class Validator extends jspb.Message {
  getValue(): string;
  setValue(value: string): void;

  getComparsontype(): ComparsonTypeMap[keyof ComparsonTypeMap];
  setComparsontype(value: ComparsonTypeMap[keyof ComparsonTypeMap]): void;

  getDatavaluetypeid(): string;
  setDatavaluetypeid(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): Validator.AsObject;
  static toObject(includeInstance: boolean, msg: Validator): Validator.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: Validator, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): Validator;
  static deserializeBinaryFromReader(message: Validator, reader: jspb.BinaryReader): Validator;
}

export namespace Validator {
  export type AsObject = {
    value: string,
    comparsontype: ComparsonTypeMap[keyof ComparsonTypeMap],
    datavaluetypeid: string,
  }
}

export class AddColumnRequest extends jspb.Message {
  getDbname(): string;
  setDbname(value: string): void;

  getTablename(): string;
  setTablename(value: string): void;

  getColumnname(): string;
  setColumnname(value: string): void;

  getDatavaluetypeid(): string;
  setDatavaluetypeid(value: string): void;

  clearValidatorsList(): void;
  getValidatorsList(): Array<Validator>;
  setValidatorsList(value: Array<Validator>): void;
  addValidators(value?: Validator, index?: number): Validator;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): AddColumnRequest.AsObject;
  static toObject(includeInstance: boolean, msg: AddColumnRequest): AddColumnRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: AddColumnRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): AddColumnRequest;
  static deserializeBinaryFromReader(message: AddColumnRequest, reader: jspb.BinaryReader): AddColumnRequest;
}

export namespace AddColumnRequest {
  export type AsObject = {
    dbname: string,
    tablename: string,
    columnname: string,
    datavaluetypeid: string,
    validatorsList: Array<Validator.AsObject>,
  }
}

export class DropColumnRequst extends jspb.Message {
  getDbname(): string;
  setDbname(value: string): void;

  getTablename(): string;
  setTablename(value: string): void;

  getColumnname(): string;
  setColumnname(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): DropColumnRequst.AsObject;
  static toObject(includeInstance: boolean, msg: DropColumnRequst): DropColumnRequst.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: DropColumnRequst, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): DropColumnRequst;
  static deserializeBinaryFromReader(message: DropColumnRequst, reader: jspb.BinaryReader): DropColumnRequst;
}

export namespace DropColumnRequst {
  export type AsObject = {
    dbname: string,
    tablename: string,
    columnname: string,
  }
}

export class DeleteRequest extends jspb.Message {
  getDbname(): string;
  setDbname(value: string): void;

  getTablename(): string;
  setTablename(value: string): void;

  clearGuidsList(): void;
  getGuidsList(): Array<string>;
  setGuidsList(value: Array<string>): void;
  addGuids(value: string, index?: number): string;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): DeleteRequest.AsObject;
  static toObject(includeInstance: boolean, msg: DeleteRequest): DeleteRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: DeleteRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): DeleteRequest;
  static deserializeBinaryFromReader(message: DeleteRequest, reader: jspb.BinaryReader): DeleteRequest;
}

export namespace DeleteRequest {
  export type AsObject = {
    dbname: string,
    tablename: string,
    guidsList: Array<string>,
  }
}

export class Row extends jspb.Message {
  clearItemsList(): void;
  getItemsList(): Array<string>;
  setItemsList(value: Array<string>): void;
  addItems(value: string, index?: number): string;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): Row.AsObject;
  static toObject(includeInstance: boolean, msg: Row): Row.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: Row, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): Row;
  static deserializeBinaryFromReader(message: Row, reader: jspb.BinaryReader): Row;
}

export namespace Row {
  export type AsObject = {
    itemsList: Array<string>,
  }
}

export class InsertRequest extends jspb.Message {
  getDbname(): string;
  setDbname(value: string): void;

  getTablename(): string;
  setTablename(value: string): void;

  clearRowsList(): void;
  getRowsList(): Array<Row>;
  setRowsList(value: Array<Row>): void;
  addRows(value?: Row, index?: number): Row;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): InsertRequest.AsObject;
  static toObject(includeInstance: boolean, msg: InsertRequest): InsertRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: InsertRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): InsertRequest;
  static deserializeBinaryFromReader(message: InsertRequest, reader: jspb.BinaryReader): InsertRequest;
}

export namespace InsertRequest {
  export type AsObject = {
    dbname: string,
    tablename: string,
    rowsList: Array<Row.AsObject>,
  }
}

export class ConditionsFieldEntry extends jspb.Message {
  getKey(): string;
  setKey(value: string): void;

  clearValueList(): void;
  getValueList(): Array<Validator>;
  setValueList(value: Array<Validator>): void;
  addValue(value?: Validator, index?: number): Validator;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): ConditionsFieldEntry.AsObject;
  static toObject(includeInstance: boolean, msg: ConditionsFieldEntry): ConditionsFieldEntry.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: ConditionsFieldEntry, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): ConditionsFieldEntry;
  static deserializeBinaryFromReader(message: ConditionsFieldEntry, reader: jspb.BinaryReader): ConditionsFieldEntry;
}

export namespace ConditionsFieldEntry {
  export type AsObject = {
    key: string,
    valueList: Array<Validator.AsObject>,
  }
}

export class UpdateRequest extends jspb.Message {
  getDbname(): string;
  setDbname(value: string): void;

  getTablename(): string;
  setTablename(value: string): void;

  clearRowsList(): void;
  getRowsList(): Array<Row>;
  setRowsList(value: Array<Row>): void;
  addRows(value?: Row, index?: number): Row;

  clearConditionsList(): void;
  getConditionsList(): Array<ConditionsFieldEntry>;
  setConditionsList(value: Array<ConditionsFieldEntry>): void;
  addConditions(value?: ConditionsFieldEntry, index?: number): ConditionsFieldEntry;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): UpdateRequest.AsObject;
  static toObject(includeInstance: boolean, msg: UpdateRequest): UpdateRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: UpdateRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): UpdateRequest;
  static deserializeBinaryFromReader(message: UpdateRequest, reader: jspb.BinaryReader): UpdateRequest;
}

export namespace UpdateRequest {
  export type AsObject = {
    dbname: string,
    tablename: string,
    rowsList: Array<Row.AsObject>,
    conditionsList: Array<ConditionsFieldEntry.AsObject>,
  }
}

export class SelectRequest extends jspb.Message {
  getDbname(): string;
  setDbname(value: string): void;

  getTablename(): string;
  setTablename(value: string): void;

  clearConditionsList(): void;
  getConditionsList(): Array<ConditionsFieldEntry>;
  setConditionsList(value: Array<ConditionsFieldEntry>): void;
  addConditions(value?: ConditionsFieldEntry, index?: number): ConditionsFieldEntry;

  getShowsyscolumns(): boolean;
  setShowsyscolumns(value: boolean): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): SelectRequest.AsObject;
  static toObject(includeInstance: boolean, msg: SelectRequest): SelectRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: SelectRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): SelectRequest;
  static deserializeBinaryFromReader(message: SelectRequest, reader: jspb.BinaryReader): SelectRequest;
}

export namespace SelectRequest {
  export type AsObject = {
    dbname: string,
    tablename: string,
    conditionsList: Array<ConditionsFieldEntry.AsObject>,
    showsyscolumns: boolean,
  }
}

export class InnerJoinRequest extends jspb.Message {
  getDbname(): string;
  setDbname(value: string): void;

  getFirsttablename(): string;
  setFirsttablename(value: string): void;

  getSecondtablename(): string;
  setSecondtablename(value: string): void;

  getFirstcolumnname(): string;
  setFirstcolumnname(value: string): void;

  getSecondcolumnname(): string;
  setSecondcolumnname(value: string): void;

  getShowsyscolumns(): boolean;
  setShowsyscolumns(value: boolean): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): InnerJoinRequest.AsObject;
  static toObject(includeInstance: boolean, msg: InnerJoinRequest): InnerJoinRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: InnerJoinRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): InnerJoinRequest;
  static deserializeBinaryFromReader(message: InnerJoinRequest, reader: jspb.BinaryReader): InnerJoinRequest;
}

export namespace InnerJoinRequest {
  export type AsObject = {
    dbname: string,
    firsttablename: string,
    secondtablename: string,
    firstcolumnname: string,
    secondcolumnname: string,
    showsyscolumns: boolean,
  }
}

export class CrossJoinRequest extends jspb.Message {
  getDbname(): string;
  setDbname(value: string): void;

  getFirsttablename(): string;
  setFirsttablename(value: string): void;

  getSecondtablename(): string;
  setSecondtablename(value: string): void;

  getShowsyscolumns(): boolean;
  setShowsyscolumns(value: boolean): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): CrossJoinRequest.AsObject;
  static toObject(includeInstance: boolean, msg: CrossJoinRequest): CrossJoinRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: CrossJoinRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): CrossJoinRequest;
  static deserializeBinaryFromReader(message: CrossJoinRequest, reader: jspb.BinaryReader): CrossJoinRequest;
}

export namespace CrossJoinRequest {
  export type AsObject = {
    dbname: string,
    firsttablename: string,
    secondtablename: string,
    showsyscolumns: boolean,
  }
}

export class SelectReply extends jspb.Message {
  getCode(): number;
  setCode(value: number): void;

  getMessage(): string;
  setMessage(value: string): void;

  getStacktrace(): string;
  setStacktrace(value: string): void;

  clearRowsList(): void;
  getRowsList(): Array<Row>;
  setRowsList(value: Array<Row>): void;
  addRows(value?: Row, index?: number): Row;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): SelectReply.AsObject;
  static toObject(includeInstance: boolean, msg: SelectReply): SelectReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: SelectReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): SelectReply;
  static deserializeBinaryFromReader(message: SelectReply, reader: jspb.BinaryReader): SelectReply;
}

export namespace SelectReply {
  export type AsObject = {
    code: number,
    message: string,
    stacktrace: string,
    rowsList: Array<Row.AsObject>,
  }
}

export class CreateDbRequest extends jspb.Message {
  getName(): string;
  setName(value: string): void;

  getRootpath(): string;
  setRootpath(value: string): void;

  getFilesize(): number;
  setFilesize(value: number): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): CreateDbRequest.AsObject;
  static toObject(includeInstance: boolean, msg: CreateDbRequest): CreateDbRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: CreateDbRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): CreateDbRequest;
  static deserializeBinaryFromReader(message: CreateDbRequest, reader: jspb.BinaryReader): CreateDbRequest;
}

export namespace CreateDbRequest {
  export type AsObject = {
    name: string,
    rootpath: string,
    filesize: number,
  }
}

export class TableRequest extends jspb.Message {
  getDbname(): string;
  setDbname(value: string): void;

  getTablename(): string;
  setTablename(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): TableRequest.AsObject;
  static toObject(includeInstance: boolean, msg: TableRequest): TableRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: TableRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): TableRequest;
  static deserializeBinaryFromReader(message: TableRequest, reader: jspb.BinaryReader): TableRequest;
}

export namespace TableRequest {
  export type AsObject = {
    dbname: string,
    tablename: string,
  }
}

export class GetTableListRequest extends jspb.Message {
  getDbname(): string;
  setDbname(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetTableListRequest.AsObject;
  static toObject(includeInstance: boolean, msg: GetTableListRequest): GetTableListRequest.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: GetTableListRequest, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetTableListRequest;
  static deserializeBinaryFromReader(message: GetTableListRequest, reader: jspb.BinaryReader): GetTableListRequest;
}

export namespace GetTableListRequest {
  export type AsObject = {
    dbname: string,
  }
}

export class GetTableListReply extends jspb.Message {
  getCode(): number;
  setCode(value: number): void;

  getMessage(): string;
  setMessage(value: string): void;

  getStacktrace(): string;
  setStacktrace(value: string): void;

  clearTablesList(): void;
  getTablesList(): Array<string>;
  setTablesList(value: Array<string>): void;
  addTables(value: string, index?: number): string;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): GetTableListReply.AsObject;
  static toObject(includeInstance: boolean, msg: GetTableListReply): GetTableListReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: GetTableListReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): GetTableListReply;
  static deserializeBinaryFromReader(message: GetTableListReply, reader: jspb.BinaryReader): GetTableListReply;
}

export namespace GetTableListReply {
  export type AsObject = {
    code: number,
    message: string,
    stacktrace: string,
    tablesList: Array<string>,
  }
}

export class BaseReply extends jspb.Message {
  getCode(): number;
  setCode(value: number): void;

  getMessage(): string;
  setMessage(value: string): void;

  getStacktrace(): string;
  setStacktrace(value: string): void;

  serializeBinary(): Uint8Array;
  toObject(includeInstance?: boolean): BaseReply.AsObject;
  static toObject(includeInstance: boolean, msg: BaseReply): BaseReply.AsObject;
  static extensions: {[key: number]: jspb.ExtensionFieldInfo<jspb.Message>};
  static extensionsBinary: {[key: number]: jspb.ExtensionFieldBinaryInfo<jspb.Message>};
  static serializeBinaryToWriter(message: BaseReply, writer: jspb.BinaryWriter): void;
  static deserializeBinary(bytes: Uint8Array): BaseReply;
  static deserializeBinaryFromReader(message: BaseReply, reader: jspb.BinaryReader): BaseReply;
}

export namespace BaseReply {
  export type AsObject = {
    code: number,
    message: string,
    stacktrace: string,
  }
}

export interface ComparsonTypeMap {
  COMPARSON_TYPE_EQUAL: 0;
  COMPARSON_TYPE_NOT_EQUAL: 1;
  COMPARSON_TYPE_GREATER: 2;
  COMPARSON_TYPE_GREATER_OR_EQUAL: 3;
  COMPARSON_TYPE_LESS: 4;
  COMPARSON_TYPE_LESS_OR_EQUAL: 5;
  COMPARSON_TYPE_STARTS_WITH: 6;
  COMPARSON_TYPE_NOT_STARTS_WITH: 7;
  COMPARSON_TYPE_CONTAINS: 8;
  COMPARSON_TYPE_NOT_CONTAINS: 9;
  COMPARSON_TYPE_IS_NULL: 10;
  COMPARSON_TYPE_IS_NOT_NULL: 11;
  COMPARSON_TYPE_ENDS_WITH: 12;
  COMPARSON_TYPE_NOT_ENDS_WITH: 13;
}

export const ComparsonType: ComparsonTypeMap;

