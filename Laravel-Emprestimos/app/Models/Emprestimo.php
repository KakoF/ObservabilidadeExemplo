<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Emprestimo extends Model
{
    use HasFactory;

    protected $table = 'emprestimos';

    protected $fillable = [
        'nome_solicitante',
        'valor',
        'parcelas',
        'taxa_juros',
        'status',
        'data_solicitacao',
        'data_aprovacao',
        'observacoes'
    ];

    protected $casts = [
        'valor' => 'decimal:2',
        'taxa_juros' => 'decimal:2',
        'data_solicitacao' => 'date',
        'data_aprovacao' => 'date',
    ];

    protected $attributes = [
        'status' => 'pendente',
    ];
}
