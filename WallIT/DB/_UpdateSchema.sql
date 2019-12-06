alter table record add column record_category_id int4;
alter table record add column account_id int4;
create table record_template (id  serial, name varchar(255), amount float8, creation_date_utc timestamp, modification_date_utc timestamp, record_category_id int4, account_id int4, primary key (id));
alter table record add constraint fk_record_category_record foreign key (record_category_id) references record_category;
alter table record add constraint fk_account_record foreign key (account_id) references account;
create index ix_record_record_category_id on record (record_category_id);
create index ix_record_account_id on record (account_id);
alter table record_template add constraint fk_record_category_record_template foreign key (record_category_id) references record_category;
alter table record_template add constraint fk_account_record_template foreign key (account_id) references account;
create index ix_record_template_record_category_id on record_template (record_category_id);
create index ix_record_template_account_id on record_template (account_id)