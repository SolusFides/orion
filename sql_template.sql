CREATE TABLE `USER`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'ID из Ujin',
    `name` VARCHAR(255) NOT NULL COMMENT 'имя сотрудника',
    `ujin_token` VARCHAR(255) NOT NULL COMMENT 'активный токен',
    `role` VARCHAR(255) NOT NULL COMMENT 'роль(admin)',
    `created_at` DATETIME NOT NULL
);
CREATE TABLE `EMERGENCY`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'UUID',
    `text` VARCHAR(255) NOT NULL COMMENT 'текст сообщения чс',
    `target_screens` JSON NOT NULL COMMENT 'массив ID экранов или [all]',
    `is_active` BOOLEAN NOT NULL COMMENT 'статус активности',
    `created_at` DATETIME NOT NULL,
    `activated_by` VARCHAR(255) NULL COMMENT 'ID пользователя из USER'
);
CREATE TABLE `TEMPLATE`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'UUID',
    `name` VARCHAR(255) NOT NULL COMMENT 'название шаблона',
    `config` JSON NOT NULL COMMENT 'все настройки: сетка, виджеты, ткма, фон',
    `deleted_at` DATETIME NOT NULL
);
CREATE TABLE `SCREEN`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT PRIMARY KEY COMMENT 'уникальный ID',
    `name` VARCHAR(255) NOT NULL COMMENT 'название',
    `complex_id` INT NOT NULL COMMENT 'ID ЖК из UJin',
    `building_id` INT NOT NULL COMMENT 'ID ЖК из Ujin',
    `current_template_id` VARCHAR(255) NOT NULL COMMENT 'опционально'
);
ALTER TABLE
    `EMERGENCY` ADD CONSTRAINT `emergency_activated_by_foreign` FOREIGN KEY(`activated_by`) REFERENCES `USER`(`id`);
ALTER TABLE
    `SCREEN` ADD CONSTRAINT `screen_current_template_id_foreign` FOREIGN KEY(`current_template_id`) REFERENCES `TEMPLATE`(`id`);