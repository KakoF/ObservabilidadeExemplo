<?php

namespace App\Http\Controllers\Api;

use App\Http\Controllers\Controller;
use App\Models\Emprestimo;
use Illuminate\Http\Request;
use Illuminate\Http\Response;

class EmprestimoController extends Controller
{
    public function index()
    {
        $emprestimos = Emprestimo::all();
        return response()->json($emprestimos);
    }

    public function store(Request $request)
    {
        $validated = $request->validate([
            'nome_solicitante' => 'required|string|max:255',
            'valor' => 'required|numeric|min:0',
            'parcelas' => 'required|integer|min:1',
            'taxa_juros' => 'required|numeric|min:0',
            'data_solicitacao' => 'required|date',
            'observacoes' => 'nullable|string'
        ]);

        $emprestimo = Emprestimo::create($validated);

        return response()->json($emprestimo, Response::HTTP_CREATED);
    }

    public function show($id)
    {
        $emprestimo = Emprestimo::findOrFail($id);
        return response()->json($emprestimo);
    }

    public function update(Request $request, $id)
    {
        $emprestimo = Emprestimo::findOrFail($id);

        $validated = $request->validate([
            'nome_solicitante' => 'sometimes|required|string|max:255',
            'valor' => 'sometimes|required|numeric|min:0',
            'parcelas' => 'sometimes|required|integer|min:1',
            'taxa_juros' => 'sometimes|required|numeric|min:0',
            'status' => 'sometimes|required|in:pendente,aprovado,reprovado,quitado',
            'data_solicitacao' => 'sometimes|required|date',
            'data_aprovacao' => 'sometimes|nullable|date',
            'observacoes' => 'nullable|string'
        ]);

        $emprestimo->update($validated);

        return response()->json($emprestimo);
    }

    public function destroy($id)
    {
        $emprestimo = Emprestimo::findOrFail($id);
        $emprestimo->delete();

        return response()->json(null, Response::HTTP_NO_CONTENT);
    }
}
