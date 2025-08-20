# Usa imagem oficial do PHP com extensões básicas
FROM php:8.2-cli

# Instala dependências do sistema
RUN apt-get update && apt-get install -y \
    unzip \
    git \
    curl \
    libzip-dev \
    zip \
    libpng-dev \
    libonig-dev \
    libxml2-dev \
    && docker-php-ext-install pdo pdo_mysql zip mbstring exif pcntl

# Instala Composer
COPY --from=composer:latest /usr/bin/composer /usr/bin/composer

# Define diretório de trabalho
WORKDIR /var/www

# Copia os arquivos da aplicação
COPY . .

# Instala dependências do Laravel
RUN composer install --no-interaction --prefer-dist --optimize-autoloader

# Dá permissões (ajuste conforme seu usuário)
RUN chown -R www-data:www-data /var/www

# Expõe a porta usada pelo Laravel
EXPOSE 8000

# Comando padrão ao iniciar o container
CMD ["php", "artisan", "serve", "--host=0.0.0.0", "--port=8000"]