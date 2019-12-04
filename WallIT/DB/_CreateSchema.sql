create table account (id  serial, name varchar(255), balance float8, account_type varchar(255), currency varchar(255), creation_date_utc timestamp, modification_date_utc timestamp, user_id int4, primary key (id));
create table credit_card (id  serial, name varchar(255) not null, creation_date_utc timestamp, modification_date_utc timestamp, primary key (id));
create table record_category (id  serial, name varchar(255), last_used_utc timestamp, creation_date_utc timestamp, modification_date_utc timestamp, parent_category_id int4, primary key (id));
create table record (id  serial, amount float8, currency varchar(255), transaction_date_utc timestamp, creation_date_utc timestamp, modification_date_utc timestamp, primary key (id));
create table user_claim (id  serial, claim_type varchar(255), claim_value varchar(255), creation_date_utc timestamp, modification_date_utc timestamp, user_id int4, primary key (id));
create table "user" (id  serial, name varchar(255), user_name varchar(255), normalized_user_name varchar(255), password_hash varchar(255), email varchar(255), last_attempt_utc timestamp, access_failed_count int4, last_logged_in_utc timestamp, is_last_login_persistent boolean, security_stamp varchar(255), lockout_enabled boolean, lockout_end timestamp, creation_date_utc timestamp, modification_date_utc timestamp, primary key (id));
create index ix_account_user_id on account (user_id);
alter table account add constraint fk_user_account foreign key (user_id) references "user";
create index ix_record_category_parent_category_id on record_category (parent_category_id);
alter table record_category add constraint fk_parent_category_record_category foreign key (parent_category_id) references record_category;
create index ix_user_claim_user_id on user_claim (user_id);
alter table user_claim add constraint fk_user_user_claim foreign key (user_id) references "user"