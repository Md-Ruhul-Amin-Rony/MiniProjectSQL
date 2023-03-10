





ALTER TABLE "public"."mra_project_person" DROP CONSTRAINT "FK_mra_person_project_person_id";
ALTER TABLE "public"."mra_project_person" DROP CONSTRAINT "FK_mra_project_person_project_id";
DROP TABLE IF EXISTS "public"."mra_person";
DROP TABLE IF EXISTS "public"."mra_project";
DROP TABLE IF EXISTS "public"."mra_project_person";
CREATE TABLE "public"."mra_person" ( 
  "id" SERIAL,
  "person_name" VARCHAR(25) NOT NULL,
  CONSTRAINT "mra_person_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."mra_project" ( 
  "id" SERIAL,
  "project_name" VARCHAR(50) NOT NULL,
  CONSTRAINT "mra_project_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."mra_project_person" ( 
  "id" SERIAL,
  "project_id" INTEGER NOT NULL,
  "person_id" INTEGER NOT NULL,
  "hours" INTEGER NOT NULL,
  CONSTRAINT "mra_project_person_pkey" PRIMARY KEY ("id")
);
ALTER TABLE "public"."mra_person" DISABLE TRIGGER ALL;
ALTER TABLE "public"."mra_project" DISABLE TRIGGER ALL;
ALTER TABLE "public"."mra_project_person" DISABLE TRIGGER ALL;
INSERT INTO "public"."mra_person" ("person_name") VALUES ('nicklas wallman');
INSERT INTO "public"."mra_person" ("person_name") VALUES ('sahid afridi');
INSERT INTO "public"."mra_person" ("person_name") VALUES ('mamun');
INSERT INTO "public"."mra_person" ("person_name") VALUES ('hasan');
INSERT INTO "public"."mra_person" ("person_name") VALUES ('nuruz');
INSERT INTO "public"."mra_person" ("person_name") VALUES ('ruhul amin');
INSERT INTO "public"."mra_person" ("person_name") VALUES ('ovhi');
INSERT INTO "public"."mra_person" ("person_name") VALUES ('krille');
INSERT INTO "public"."mra_project" ("project_name") VALUES ('hospital');
INSERT INTO "public"."mra_project" ("project_name") VALUES ('school');
INSERT INTO "public"."mra_project" ("project_name") VALUES ('university');
INSERT INTO "public"."mra_project" ("project_name") VALUES ('car');
INSERT INTO "public"."mra_project" ("project_name") VALUES ('house');
INSERT INTO "public"."mra_project" ("project_name") VALUES ('badminton');
INSERT INTO "public"."mra_project_person" ("project_id", "person_id", "hours") VALUES (3, 5, 50);
INSERT INTO "public"."mra_project_person" ("project_id", "person_id", "hours") VALUES (2, 2, 30);
INSERT INTO "public"."mra_project_person" ("project_id", "person_id", "hours") VALUES (2, 3, 20);
INSERT INTO "public"."mra_project_person" ("project_id", "person_id", "hours") VALUES (2, 7, 30);
INSERT INTO "public"."mra_project_person" ("project_id", "person_id", "hours") VALUES (4, 7, 20);
INSERT INTO "public"."mra_project_person" ("project_id", "person_id", "hours") VALUES (5, 6, 20);
ALTER TABLE "public"."mra_person" ENABLE TRIGGER ALL;
ALTER TABLE "public"."mra_project" ENABLE TRIGGER ALL;
ALTER TABLE "public"."mra_project_person" ENABLE TRIGGER ALL;
ALTER TABLE "public"."mra_project_person" ADD CONSTRAINT "FK_mra_project_person_project_id" FOREIGN KEY ("project_id") REFERENCES "public"."mra_project" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."mra_project_person" ADD CONSTRAINT "FK_mra_person_project_person_id" FOREIGN KEY ("person_id") REFERENCES "public"."mra_person" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;

