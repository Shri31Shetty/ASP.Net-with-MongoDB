@echo off
(
echo ===== Models\IStudentStoreDatabaseSettings.cs =====
type "Models\IStudentStoreDatabaseSettings.cs"
echo.

echo ===== Models\Student.cs =====
type "Models\Student.cs"
echo.

echo ===== Models\StudentStoreDatabaseSettings.cs =====
type "Models\StudentStoreDatabaseSettings.cs"
echo.

echo ===== Models\SimpleFileLogger.cs =====
type "Models\SimpleFileLogger.cs"
echo.

echo ===== Models\SimpleFileLoggerProvider.cs =====
type "Models\SimpleFileLoggerProvider.cs"
echo.

echo ===== Repository\IStudentRepository.cs =====
type "Repository\IStudentRepository.cs"
echo.

echo ===== Repository\StudentRepository.cs =====
type "Repository\StudentRepository.cs"
echo.

echo ===== StudentManagement.Services\IStudentService.cs =====
type "StudentManagement.Services\IStudentService.cs"
echo.

echo ===== StudentManagement.Services\StudentService.cs =====
type "StudentManagement.Services\StudentService.cs"
echo.

echo ===== StudentManagement\Program.cs =====
type "StudentManagement\Program.cs"
echo.

echo ===== StudentManagement\Controllers\AuthController.cs =====
type "StudentManagement\Controllers\AuthController.cs"
echo.

echo ===== StudentManagement\Controllers\StudentsController.cs =====
type "StudentManagement\Controllers\StudentsController.cs"
echo.

echo ===== Validator\StudentCreateDtoValidator.cs =====
type "Validator\StudentCreateDtoValidator.cs"
echo.

echo ===== Controller\StudentController.cs =====
type "Controller\StudentController.cs"
echo.

echo ===== Models.Test\ModelsTest.cs =====
type "Models.Test\ModelsTest.cs"
echo.

echo ===== Repository.Test\RepositoryTest.cs =====
type "Repository.Test\RepositoryTest.cs"
echo.

echo ===== Services.Test\ServicesTest.cs =====
type "Services.Test\ServicesTest.cs"
echo.

echo ===== StudentManagement.Tests\StudentServiceTests.cs =====
type "StudentManagement.Tests\StudentServiceTests.cs"
echo.

echo ===== StudentManagement.Tests\UnitTest1.cs =====
type "StudentManagement.Tests\UnitTest1.cs"
echo.

echo ===== StudentManagment.API.Test\StudentManagementAPITest.cs =====
type "StudentManagment.API.Test\StudentManagementAPITest.cs"
echo.
) > all_student_cs_contents.txt
