ALTER TABLE "public"."mra_project_person" DROP CONSTRAINT "FK_mra_project_person_project_id";
ALTER TABLE "public"."mra_project_person" DROP CONSTRAINT "FK_mra_person_project_person_id";
DROP TABLE IF EXISTS "public"."mra_project";
DROP TABLE IF EXISTS "public"."mra_person";
DROP TABLE IF EXISTS "public"."mra_project_person";
CREATE TABLE "public"."mra_project" ( 
  "id" SERIAL,
  "project_name" VARCHAR(50) NOT NULL,
  CONSTRAINT "mra_project_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."mra_person" ( 
  "id" SERIAL,
  "person_name" VARCHAR(25) NOT NULL,
  CONSTRAINT "mra_person_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."mra_project_person" ( 
  "id" SERIAL,
  "project_id" INTEGER NOT NULL,
  "person_id" INTEGER NOT NULL,
  "hours" INTEGER NOT NULL,
  CONSTRAINT "mra_project_person_pkey" PRIMARY KEY ("id")
);
ALTER TABLE "public"."mra_project_person" ADD CONSTRAINT "FK_mra_project_person_project_id" FOREIGN KEY ("project_id") REFERENCES "public"."mra_project"("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."mra_project_person" ADD CONSTRAINT "FK_mra_person_project_person_id" FOREIGN KEY ("person_id") REFERENCES "public"."mra_person"("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
