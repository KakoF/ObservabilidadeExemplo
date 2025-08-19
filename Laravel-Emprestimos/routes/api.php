<?php

use App\Http\Controllers\Api\EmprestimoController;
use Illuminate\Support\Facades\Route;

Route::apiResource('emprestimos', EmprestimoController::class);
