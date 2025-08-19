<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('emprestimos', function (Blueprint $table) {
            $table->id();
            $table->string('nome_solicitante');
            $table->decimal('valor', 10, 2);
            $table->integer('parcelas');
            $table->decimal('taxa_juros', 5, 2);
            $table->enum('status', ['pendente', 'aprovado', 'reprovado', 'quitado'])->default('pendente');
            $table->date('data_solicitacao');
            $table->date('data_aprovacao')->nullable();
            $table->text('observacoes')->nullable();
            $table->timestamps();
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('emprestimos');
    }
};
